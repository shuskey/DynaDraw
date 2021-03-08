using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.DataObjects;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Web;

public class StartUpInitialization : MonoBehaviour
{
    public Camera mainCamera;
    public float defaultFieldOfView = 60.0f;
    public Light directionalLight;
    public Canvas canvasObject;
    public Text movingTitlePrefab;
    public Text movingSubtitlePrefab;
    public Dropdown dropdown;
    public InputField inputFieldTitle;
    public InputField inputFieldCommands;
    public GameObject AllKeyboardsGameObject;    
    public Button clearButton;
    public Button saveButton;
    public Button changeSceneButton;
    public Button pauseButton;
    public Button helpButton;
    public string instructionsUrl;
    public Button shareButton;
    public Slider speedSlider;
    public Slider fieldOfViewSlider;
    public Toggle showKeyboardToggle;
    public Button homeMenuButton;
    public Button showMenuButton;
    public Button nextCreationButton;
    public Button previousCreationButton;
    public bool animationPaused = false;
    public GameObject drawStringObject;
    public List<string> sceneTitles;
    public List<Material> skyBoxMaterial;
    public List<float> sceneIntensity;
    public Animator transitionAnim;
    private float initialFieldOfView;
    private float initialSpeed;
    private string baseUrl;
    private DynaDrawOriginalCreations dynaDrawOriginalCreations;
    private DynaDrawSavedCreations dynaDrawSavedCreations;
    private SceneDefinitionPresets sceneDefinitionPresets;
    private string currentSceneName = "";
    private bool skyBoxMaterialChanged = false;
    private int currentDropdownSelectionIndex = 0;
    private int savedCursorPosition = 0;
    private bool showMenuToggle = false;
    private string transitionSubtitle;
    private string transitionTitle;
    private bool dynaCreationReadytoChange = false;

#if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string str);

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);
#endif
    enum TagsICareAbout { NoHide, InverseHide, Keyboard }

    private void Awake()
    {        
        sceneDefinitionPresets = new SceneDefinitionPresets();
        int index = 0;
        foreach (var sceneTitle in sceneTitles)
        {
            SceneDefinition addThisScene = new SceneDefinition(sceneTitles[index], skyBoxMaterial[index], sceneIntensity[index]);
            sceneDefinitionPresets.AddPreset(addThisScene);
            index++;
        }
        var textEditTitle = inputFieldTitle.transform.GetComponent<InputField>();
        textEditTitle.onValueChanged.AddListener(delegate { TextEditTitleValueChanged(); });
        var textEditCommands = inputFieldCommands.transform.GetComponent<InputField>();
        textEditCommands.onValueChanged.AddListener(delegate { TextEditCommandsValueChanged(); });
        saveButton.GetComponentInChildren<Text>().text = "Save";
        saveButton.interactable = false;
        clearButton.onClick.AddListener(delegate { ClearButtonClicked(); });
        saveButton.onClick.AddListener(delegate { SaveButtonClicked(); });
        pauseButton.onClick.AddListener(delegate { PauseButtonClicked(); });
        changeSceneButton.onClick.AddListener(delegate { ChangeSceneButtonClicked(); });
        shareButton.onClick.AddListener(delegate { ShareButtonClicked(); });
        helpButton.onClick.AddListener(delegate { HelpButtonClicked(); });
        dropdown.onValueChanged.AddListener(delegate { DropDownValueChanged(dropdown.value); });
        previousCreationButton.onClick.AddListener(delegate { DropDownValueIncrement(-1); });
        nextCreationButton.onClick.AddListener(delegate { DropDownValueIncrement(1); });        
        speedSlider.onValueChanged.AddListener(delegate { SpeedSliderChanged(speedSlider.value); });
        fieldOfViewSlider.onValueChanged.AddListener(delegate { FieldOfViewSliderChanged(fieldOfViewSlider.value); });
        showMenuButton.onClick.AddListener(delegate { showMenuClicked(); });
        homeMenuButton.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
        showKeyboardToggle.onValueChanged.AddListener(delegate { ShowKeyboardToggleChanged(showKeyboardToggle.isOn); });

        initialFieldOfView = mainCamera.fieldOfView;
        initialSpeed = Time.timeScale;

        AllKeyboardsGameObject.SetActive(false);        
    }

    void StartMovingTitles(string title, string subtitle)
    {
        var movingTitle = Instantiate(movingTitlePrefab, canvasObject.transform);
        //movingTitle.transform.SetParent(canvasObject.transform);
        movingTitle.text = title; 
        var movingSubtitle = Instantiate(movingSubtitlePrefab, canvasObject.transform);
        //movingSubtitle.transform.SetParent(canvasObject.transform);
        movingSubtitle.text = subtitle;
    }

    void TextEditTitleValueChanged()
    {
        saveButton.GetComponentInChildren<Text>().text = "Save";
        saveButton.interactable = true;
    }

    void TextEditCommandsValueChanged()
    {
        SaveOrUpdatableActionOccured();
    }

    void SaveOrUpdatableActionOccured()
    {
        if (currentDropdownSelectionIndex < dynaDrawOriginalCreations.OriginalCreationsList.Count) //Editing an Original Creation -> potentially save it as new       
            saveButton.GetComponentInChildren<Text>().text = "Save";
        if (saveButton.GetComponentInChildren<Text>().text != "Save")  // if title was changed, this will still be a save
            saveButton.GetComponentInChildren<Text>().text = "Update";
        saveButton.interactable = true;
    }

    void DropDownValueChanged(int index)
    {
        currentDropdownSelectionIndex = index;
        ChangeCreationInAScene(index);
    }

    void DropDownValueIncrement(int incrementOrDecrement)
    {
        currentDropdownSelectionIndex += incrementOrDecrement;

        if (currentDropdownSelectionIndex < 0)
            currentDropdownSelectionIndex = dropdown.options.Count - 1;
        if (currentDropdownSelectionIndex >= dropdown.options.Count)
            currentDropdownSelectionIndex = 0;

        StartCoroutine(DelayThenSwitchToThisCreation());
        StartCoroutine(TransitionToBlackAndBack());
    }

    void ChangeCreationInAScene(int index)
    { 
        var drawStringScript = drawStringObject.GetComponentInChildren<DrawStringScript>();
        var originalCreationsListSize = dynaDrawOriginalCreations.OriginalCreationsList.Count;
        currentDropdownSelectionIndex = index;

        if (index < originalCreationsListSize)  // Prefabs original creations - not editable, nor deletable
        {
            var selectedDynaDrawOriginalItem = dynaDrawOriginalCreations.OriginalCreationsList[index];
            inputFieldTitle.text = selectedDynaDrawOriginalItem.Title;
            drawStringScript.SetDynaString(inputFieldCommands.text = selectedDynaDrawOriginalItem.DynaDrawCommands);

            ChangeToThisScene(sceneDefinitionPresets.getSpecific(selectedDynaDrawOriginalItem.SceneName ?? sceneTitles[0]));
            var dynaview = selectedDynaDrawOriginalItem.FieldOfView;
            var dynaspeed = selectedDynaDrawOriginalItem.TimeScale;

            if (!string.IsNullOrEmpty(dynaview))
            {
                fieldOfViewSlider.value = float.Parse(dynaview, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                //mainCamera.fieldOfView = fieldOfViewSlider.value;
            }
            else
                fieldOfViewSlider.value = defaultFieldOfView;

            if (!string.IsNullOrEmpty(dynaspeed))
            {
                speedSlider.value = float.Parse(dynaspeed, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                //Time.timeScale = speedSlider.value;
            }
            else
                speedSlider.value = 1.0f;

            saveButton.GetComponentInChildren<Text>().text = "Delete";
            saveButton.interactable = false;
            transitionTitle = selectedDynaDrawOriginalItem.Title;
            transitionSubtitle = selectedDynaDrawOriginalItem.Subtitle;
            return;
        }
        if (index >= originalCreationsListSize)  // Here is the users own saved items
        {
            var selectedDynaDrawSavedItem = dynaDrawSavedCreations.UserSaveCreationsList[index - originalCreationsListSize];
            inputFieldTitle.text = selectedDynaDrawSavedItem.Title;
            drawStringScript.SetDynaString(inputFieldCommands.text = selectedDynaDrawSavedItem.DynaDrawCommands);

            ChangeToThisScene(sceneDefinitionPresets.getSpecific(selectedDynaDrawSavedItem.SceneName ?? sceneTitles[0]));

            var dynaview = selectedDynaDrawSavedItem.FieldOfView;
            var dynaspeed = selectedDynaDrawSavedItem.TimeScale;

            if (!string.IsNullOrEmpty(dynaview))
            {
                fieldOfViewSlider.value = float.Parse(dynaview, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                //mainCamera.fieldOfView = fieldOfViewSlider.value;
            }
            else
                fieldOfViewSlider.value = defaultFieldOfView;

            if (!string.IsNullOrEmpty(dynaspeed))
            {
                speedSlider.value = float.Parse(dynaspeed, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                //Time.timeScale = speedSlider.value;
            }
            else
                speedSlider.value = 1.0f;

            saveButton.GetComponentInChildren<Text>().text = "Delete";
            saveButton.interactable = true;
            transitionTitle = selectedDynaDrawSavedItem.Title;
            transitionSubtitle = selectedDynaDrawSavedItem.Subtitle;
            return;
        }
    }

    void SpeedSliderChanged(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
        if (newTimeScale != 0 && animationPaused) //Unpause is we take this slider off zero
            PauseButtonClicked();

        SaveOrUpdatableActionOccured();
    }

    void FieldOfViewSliderChanged(float newFieldOfViewValue)
    {
        mainCamera.fieldOfView = newFieldOfViewValue;

        SaveOrUpdatableActionOccured();
    }

    void ClearButtonClicked()
    {
        saveButton.GetComponentInChildren<Text>().text = "Save";
        saveButton.interactable = false;
        dropdown.value = 0;
        currentDropdownSelectionIndex = 0;

        inputFieldCommands.text = "";
        inputFieldTitle.text = "";

        unPauseAnimation();

        fieldOfViewSlider.value = initialFieldOfView;
        //mainCamera.fieldOfView = fieldOfViewSlider.value;
        speedSlider.value = initialSpeed;
        //Time.timeScale = speedSlider.value;
    }
    
    void SaveButtonClicked()
    {
        if (saveButton.GetComponentInChildren<Text>().text == "Save")
        {
            var title = "Auto Generated Title";
            if (inputFieldTitle.text.Length != 0)
                title = inputFieldTitle.text;

            dynaDrawSavedCreations.Add(title, subtitle: "Your own creation", inputFieldCommands.text, sceneName: currentSceneName, fieldOfView: mainCamera.fieldOfView, timeScale: Time.timeScale);
            dropdown.AddOptions(new List<string>() { title });
            
            dropdown.value = currentDropdownSelectionIndex = dropdown.options.Count - 1;
             
            saveButton.GetComponentInChildren<Text>().text = "Delete";
            saveButton.interactable = false;
            return;
        }
        if (saveButton.GetComponentInChildren<Text>().text == "Update")
        {
            var title = "Auto Generated Title";
            if (inputFieldTitle.text.Length != 0)
                title = inputFieldTitle.text;

            dynaDrawSavedCreations.Update(currentDropdownSelectionIndex - dynaDrawOriginalCreations.OriginalCreationsList.Count, title, inputFieldCommands.text, sceneName: currentSceneName, fieldOfView: mainCamera.fieldOfView.ToString(), timeScale: Time.timeScale.ToString());
            saveButton.GetComponentInChildren<Text>().text = "Delete";
            saveButton.interactable = false;
            return;
        }
        if (saveButton.GetComponentInChildren<Text>().text == "Delete")
        {
            dropdown.options.RemoveAt(currentDropdownSelectionIndex);
            dynaDrawSavedCreations.Remove(currentDropdownSelectionIndex - dynaDrawOriginalCreations.OriginalCreationsList.Count);
            ClearButtonClicked();
            return;
        }
    }

    void HelpButtonClicked()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        OpenNewTab(instructionsUrl);
#else
        Application.OpenURL(instructionsUrl);
#endif
     }

    void ShareButtonClicked()
    {
        var drawStringScript = drawStringObject.GetComponentInChildren<DrawStringScript>();
        var dynaStringEncoded = System.Uri.EscapeUriString(drawStringScript.GetDynaString());
        var dynaTitleEncoded = System.Uri.EscapeUriString(inputFieldTitle.text);
        var dynaSceneNameEncoded = System.Uri.EscapeUriString(currentSceneName);
        var dynaFieldOfViewEncoded = System.Uri.EscapeUriString(mainCamera.fieldOfView.ToString()); 
        var dynaSpeedEncoded = System.Uri.EscapeUriString(Time.timeScale.ToString()); 
        var webglUrl = Application.absoluteURL;
        var stringToCopy = drawStringScript.GetDynaString();
        if (!string.IsNullOrEmpty(webglUrl))
        {
            stringToCopy = $"{baseUrl}?dynastring={dynaStringEncoded}&dynatitle={dynaTitleEncoded}";
            stringToCopy += $"&scene={dynaSceneNameEncoded}&view={dynaFieldOfViewEncoded}&speed={dynaSpeedEncoded}";

#if !UNITY_EDITOR && UNITY_WEBGL
            CopyToClipboard(stringToCopy);
            OpenNewTab(stringToCopy);
#endif
        }
        else
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            GUIUtility.systemCopyBuffer = stringToCopy;
#endif
        }
    }

    void PauseButtonClicked()
    {
        if (animationPaused)
        {
            unPauseAnimation();
        }
        else
        {                        
            Time.timeScale = 0;
            pauseButton.GetComponentInChildren<Text>().text = "Run";
        }
        animationPaused = !animationPaused;
    }

    void unPauseAnimation()
    {
        if (speedSlider.value == 0) // If we un-pause and the slider is zero, set it to 1
            speedSlider.value = 1;
        Time.timeScale = speedSlider.value;
        pauseButton.GetComponentInChildren<Text>().text = "Pause";
    }

    void ChangeSceneButtonClicked()
    {
        ChangeToThisScene(sceneDefinitionPresets.getNextPreset());

        SaveOrUpdatableActionOccured();
    }

    IEnumerator DelayThenSwitchToThisCreation()
    {
        yield return new WaitForSeconds(0.5f);
        dynaCreationReadytoChange = true;        
    }

    IEnumerator TransitionToBlackAndBack()
    {
        transitionAnim.SetTrigger("gotoblack");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeToScreenVisible());
    }

    IEnumerator FadeToScreenVisible()
    {
        transitionAnim.SetTrigger("comeoutofblack");
        yield return new WaitForSeconds(0.5f);
        StartMovingTitles(transitionTitle, transitionSubtitle);
    }

    private void ChangeToThisScene(SceneDefinition nextScene)
    {
        // var skyBoxMaterial = mainCamera.GetComponentInChildren<Skybox>().material;
        // skyBoxMaterial = nextScene.SkyBoxMaterial;
        RenderSettings.skybox = nextScene.SkyBoxMaterial;
        mainCamera.GetComponentInChildren<Skybox>().material = nextScene.SkyBoxMaterial;
        directionalLight.intensity = nextScene.DirectionalLightIntensity;

        currentSceneName = nextScene.Title;

        skyBoxMaterialChanged = true;
    }

    void ShowKeyboardToggleChanged(bool toggleIsOn)
    {
        AllKeyboardsGameObject.SetActive(toggleIsOn);
    }

    void showMenuClicked()
    {
        showMenuToggle = !showMenuToggle;
        ShowControlsToggleChanged(showMenuToggle);        
    }

    void ShowControlsToggleChanged(bool toggleIsOn)
    {
        var drawStringScript = drawStringObject.GetComponentInChildren<DrawStringScript>();
        for (int i = 0; i < canvasObject.transform.childCount - 1; i++ )
        {
            var go = canvasObject.transform.GetChild(i).gameObject;
            if (go.transform.CompareTag(TagsICareAbout.NoHide.ToString()) || go.transform.CompareTag(TagsICareAbout.Keyboard.ToString()))
            {
                continue;
            }
                
            if (go.transform.CompareTag(TagsICareAbout.InverseHide.ToString()))            {
                go.SetActive(!toggleIsOn);
                continue;
            }
            go.SetActive(toggleIsOn);            
        }
       if (toggleIsOn)
        {
            drawStringScript.Redraw(savedCursorPosition);
        }
       else
        {
            savedCursorPosition = drawStringScript.GetCursorPosition();
            drawStringScript.Redraw(-1);  // turn off cursor caret thing
        }
        if (!toggleIsOn)
            showKeyboardToggle.isOn = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        var title = "3D Dyna-Draw, by Scott Huskey";
        var subtitle = "Adapted from Michael R.Dunlavey, Performance Software Associates, Inc. (1989)";
        dynaDrawOriginalCreations = new DynaDrawOriginalCreations();
        dynaDrawSavedCreations = new DynaDrawSavedCreations();
        dynaDrawSavedCreations.GetFromJson();

        PopulateDropDown();
        
        var firstScene = sceneDefinitionPresets.getFirstPreset();

        var drawStringScript = drawStringObject.GetComponentInChildren<DrawStringScript>();

        //Check for paramerters passed on URL for Webgl versions
        var webglUrl = Application.absoluteURL;
        if (!string.IsNullOrEmpty(webglUrl))
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            var myUri = new System.Uri(webglUrl);
            var portUrlInt = myUri.Port;
            var portUrlString = "";
            if (portUrlInt != 80)
                portUrlString = $":{portUrlInt}";
            baseUrl = $"{myUri.Scheme}://{myUri.Host}{portUrlString}{myUri.LocalPath}";
            var dynastring = HttpUtility.ParseQueryString(myUri.Query).Get("dynastring");
            var dynatitle = HttpUtility.ParseQueryString(myUri.Query).Get("dynatitle");
            var dynascene = HttpUtility.ParseQueryString(myUri.Query).Get("scene");
            var dynaview = HttpUtility.ParseQueryString(myUri.Query).Get("view");
            var dynaspeed = HttpUtility.ParseQueryString(myUri.Query).Get("speed");

            if (!string.IsNullOrEmpty(dynascene))
                firstScene = sceneDefinitionPresets.getSpecific(dynascene);

            if (!string.IsNullOrEmpty(dynaview))
            {
                fieldOfViewSlider.value = float.Parse(dynaview, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                //mainCamera.fieldOfView = fieldOfViewSlider.value;
            }
            if (!string.IsNullOrEmpty(dynaspeed))
            {
                speedSlider.value = float.Parse(dynaspeed, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                //Time.timeScale = speedSlider.value;
            }

            if (string.IsNullOrEmpty(dynastring))
                dynastring = dynaDrawOriginalCreations.OriginalCreationsList[0].DynaDrawCommands;
            
            inputFieldCommands.text = dynastring;
            title = inputFieldTitle.text = dynatitle;
            subtitle = "Shared with you from a friend.";
            drawStringScript.SetDynaString(dynastring);
#endif
        }
        else
        {
            // Startup Creation is at position 0
            inputFieldCommands.text = dynaDrawOriginalCreations.OriginalCreationsList[0].DynaDrawCommands;
            drawStringScript.SetDynaString(inputFieldCommands.text);
        }
        StartMovingTitles(title, subtitle);

        ShowControlsToggleChanged(false);
        ChangeToThisScene(firstScene);
    }

    void PopulateDropDown()
    {
        dropdown.AddOptions(dynaDrawOriginalCreations.JustTitles());        
        dropdown.AddOptions(dynaDrawSavedCreations.JustTitles());
    }

    // Update is called once per frame
    void Update()
    {
        if (dynaCreationReadytoChange)
        {
            dynaCreationReadytoChange = false;
            dropdown.value = currentDropdownSelectionIndex;  // this will end up calling DropDownValueChanged
        }

        if (skyBoxMaterialChanged)
        {
            DynamicGI.UpdateEnvironment();
            skyBoxMaterialChanged = false;
        }
        
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.075f);
    }
}
