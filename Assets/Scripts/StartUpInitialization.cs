using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.DataObjects;
using System.Runtime.InteropServices;

public class StartUpInitialization : MonoBehaviour
{
    public Camera mainCamera;
    public float defaultFieldOfView = 60.0f;
    public Light directionalLight;
    public Canvas canvasObject;
    public Dropdown dropdown;
    public InputField inputFieldTitle;
    public InputField inputFieldCommands;
    public GameObject KeyboardGameObject;
    public Button keyboardKeyPrefab;
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
    public Toggle showControlsToggle;
    public bool animationPaused = false;
    public GameObject drawStringObject;
    public List<string> sceneTitles;
    public List<Material> skyBoxMaterial;
    public List<float> sceneIntensity;
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

    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string str);

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

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
        dropdown.onValueChanged.AddListener(delegate { DropDownValueChanged(dropdown.value); });
        clearButton.onClick.AddListener(delegate { ClearButtonClicked(); });
        saveButton.onClick.AddListener(delegate { SaveButtonClicked(); });
        pauseButton.onClick.AddListener(delegate { PauseButtonClicked(); });
        changeSceneButton.onClick.AddListener(delegate { ChangeSceneButtonClicked(); });
        shareButton.onClick.AddListener(delegate { ShareButtonClicked(); });
        helpButton.onClick.AddListener(delegate { HelpButtonClicked(); });
        speedSlider.onValueChanged.AddListener(delegate { SpeedSliderChanged(speedSlider.value); });
        fieldOfViewSlider.onValueChanged.AddListener(delegate { FieldOfViewSliderChanged(fieldOfViewSlider.value); });
        showControlsToggle.onValueChanged.AddListener(delegate { ShowControlsToggleChanged(showControlsToggle.isOn); });
        showKeyboardToggle.onValueChanged.AddListener(delegate { ShowKeyboardToggleChanged(showKeyboardToggle.isOn); });

        initialFieldOfView = mainCamera.fieldOfView;
        initialSpeed = Time.timeScale;
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
        var drawStringScript = drawStringObject.GetComponentInChildren<DrawStringScript>();
        var originalCreationsListSize = dynaDrawOriginalCreations.OriginalCreationsList.Count;
        currentDropdownSelectionIndex = index;
        if (index == 0)  // Selected the Title of the Dropdown
        {
            inputFieldTitle.text = "";
            drawStringScript.SetDynaString(inputFieldCommands.text = "");            

            saveButton.GetComponentInChildren<Text>().text = "Save";
            saveButton.interactable = false;
            return;
        }
        if (index < originalCreationsListSize)  // Prefabs original creations - not editable, nor deletable
        {
            var selectedDynaDrawOriginalItem = dynaDrawOriginalCreations.OriginalCreationsList[index];
            inputFieldTitle.text = selectedDynaDrawOriginalItem.Title;
            drawStringScript.SetDynaString(inputFieldCommands.text = selectedDynaDrawOriginalItem.DynaDrawCommands);
            
            ChangeToThisScene(sceneDefinitionPresets.getSpecific(selectedDynaDrawOriginalItem.SceneName ?? sceneTitles[0]));
            var dynaview =  selectedDynaDrawOriginalItem.FieldOfView;
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
            return;
        }
        if (index > originalCreationsListSize)  // Here is the users own saved items
        {
            var selectedDynaDrawSavedItem = dynaDrawSavedCreations.UserSaveCreationsList[index - originalCreationsListSize - 1];
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
        inputFieldCommands.text = "";
        inputFieldTitle.text = "";
        saveButton.GetComponentInChildren<Text>().text = "Save";
        saveButton.interactable = false;
        dropdown.value = 0;
        currentDropdownSelectionIndex = 0;

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

            dynaDrawSavedCreations.Add(title, inputFieldCommands.text, sceneName: currentSceneName, fieldOfView: mainCamera.fieldOfView, timeScale: Time.timeScale);
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

            dynaDrawSavedCreations.Update(currentDropdownSelectionIndex - dynaDrawOriginalCreations.OriginalCreationsList.Count - 1, title, inputFieldCommands.text, sceneName: currentSceneName, fieldOfView: mainCamera.fieldOfView.ToString(), timeScale: Time.timeScale.ToString());
            saveButton.GetComponentInChildren<Text>().text = "Delete";
            saveButton.interactable = false;
            return;
        }
        if (saveButton.GetComponentInChildren<Text>().text == "Delete")
        {
            dropdown.options.RemoveAt(currentDropdownSelectionIndex);
            dynaDrawSavedCreations.Remove(currentDropdownSelectionIndex - dynaDrawOriginalCreations.OriginalCreationsList.Count - 1);
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
            CopyToClipboard(stringToCopy);
        }
        else
        {
            GUIUtility.systemCopyBuffer = stringToCopy;
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
        KeyboardGameObject.SetActive(toggleIsOn);
    }

    void ShowControlsToggleChanged(bool toggleIsOn)
    {
        var drawStringScript = drawStringObject.GetComponentInChildren<DrawStringScript>();
        for (int i = 0; i < canvasObject.transform.childCount - 1; i++ )
        {
            var go = canvasObject.transform.GetChild(i).gameObject;
            if (go.transform.tag == "No Hide" || go.transform.tag == "Keyboard")
                continue;
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
        dynaDrawOriginalCreations = new DynaDrawOriginalCreations();
        dynaDrawSavedCreations = new DynaDrawSavedCreations();
        dynaDrawSavedCreations.GetFromJson();

        PopulateDropDown();
        
        PopulateOnScreenKeyboard();

        var firstScene = sceneDefinitionPresets.getFirstPreset();

        var drawStringScript = drawStringObject.GetComponentInChildren<DrawStringScript>();

        //Check for paramerters passed on URL for Webgl versions
        var webglUrl = Application.absoluteURL;
        if (!string.IsNullOrEmpty(webglUrl))
        {
            var myUri = new System.Uri(webglUrl);
            var portUrlInt = myUri.Port;
            var portUrlString = "";
            if (portUrlInt != 80)
                portUrlString = $":{portUrlInt}";
            baseUrl = $"{myUri.Scheme}://{myUri.Host}{portUrlString}{myUri.LocalPath}";
            var dynastring = System.Web.HttpUtility.ParseQueryString(myUri.Query).Get("dynastring");
            var dynatitle = System.Web.HttpUtility.ParseQueryString(myUri.Query).Get("dynatitle");
            var dynascene = System.Web.HttpUtility.ParseQueryString(myUri.Query).Get("scene");
            var dynaview = System.Web.HttpUtility.ParseQueryString(myUri.Query).Get("view");
            var dynaspeed = System.Web.HttpUtility.ParseQueryString(myUri.Query).Get("speed");

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
            inputFieldTitle.text = dynatitle;
            drawStringScript.SetDynaString(dynastring);
        }
        else
        {
            // Startup Creation is at position 0
            inputFieldCommands.text = dynaDrawOriginalCreations.OriginalCreationsList[0].DynaDrawCommands;
            drawStringScript.SetDynaString(inputFieldCommands.text);
        }

        showControlsToggle.isOn = false;
      //  ShowControlsToggleChanged(false);

        ChangeToThisScene(firstScene);
    }

    void PopulateDropDown()
    {
        dropdown.AddOptions(dynaDrawOriginalCreations.JustTitles());
        dropdown.AddOptions(new List<string>(){ "----Your Creations Below----"});
        dropdown.AddOptions(dynaDrawSavedCreations.JustTitles());
    }

    static System.Tuple<byte, string> convertToKeyDefinitionPair(char keyValue)
    {
        System.Tuple<byte, string> returnThis = new System.Tuple<byte, string>((byte)keyValue, keyValue.ToString());
        return returnThis;
    }
    static System.Tuple<byte, string>[] convertStringToKeyDefinition(string sixChars)
    {
        var returnThis = new System.Tuple<byte, string>[6];
        for (var i = 0; i < 6; i++)
        {
            if (sixChars.Substring(i, 1) == " ")  // Special Case skip this button
                returnThis[i] = new System.Tuple<byte, string>(0x00, "");
            else
                returnThis[i] = convertToKeyDefinitionPair(sixChars.Substring(i, 1).ToCharArray()[0]);
        }
        return returnThis;
    }

    private System.Tuple<byte, string>[][] allKeyDefinitionLines = new System.Tuple<byte, string>[9][];

    void PopulateOnScreenKeyboard()
    {
        allKeyDefinitionLines[0] = new System.Tuple<byte, string>[] { new System.Tuple<byte, string>( 0x1b, "<"), new System.Tuple<byte, string>( 0x1a, ">"), new System.Tuple<byte, string>( 0x20, "Sp"), new System.Tuple<byte, string>( 0x7f, "Del"), new System.Tuple<byte, string>( 0x08, "Bksp"), new System.Tuple<byte, string>( 0x00, "")  };
        allKeyDefinitionLines[1] = convertStringToKeyDefinition("01234 ");
        allKeyDefinitionLines[2] = convertStringToKeyDefinition("56789C");
        allKeyDefinitionLines[3] = convertStringToKeyDefinition("IUOiuo");
        allKeyDefinitionLines[4] = convertStringToKeyDefinition("L Rl r");
        allKeyDefinitionLines[5] = convertStringToKeyDefinition(" DZzd ");
        allKeyDefinitionLines[6] = convertStringToKeyDefinition("FMBfmb");
        allKeyDefinitionLines[7] = convertStringToKeyDefinition("SWAJKP");
        allKeyDefinitionLines[8] = convertStringToKeyDefinition("()c[]\"");
        
        var offset = new Vector3(0, 0, 0);
        int line = 0;
        foreach (var lineOfKeys in allKeyDefinitionLines)
        {
            for (var row = 0; row < lineOfKeys.Length; row++)
            {
                if (lineOfKeys[row].Item1 != 0x00)
                {
                    var keyButtonGameObject = Instantiate(keyboardKeyPrefab, KeyboardGameObject.transform.position + offset, KeyboardGameObject.transform.rotation);
                    var textElement = keyButtonGameObject.GetComponentInChildren<Text>();
                    textElement.fontSize = 12;
                    textElement.text = lineOfKeys[row].Item2;

                    keyButtonGameObject.transform.SetParent(KeyboardGameObject.transform);

                    var passThisToDelegateLine = line;
                    var passThisToDelegateRow = row;
                    keyButtonGameObject.onClick.AddListener(delegate { KeyBoardButtonClicked(passThisToDelegateLine, passThisToDelegateRow); });
                }

                offset.x += 30.0f;
            }
            offset.x = 0.0f;
            offset.y -= 30.0f;
            line++;
        }
    }

    void KeyBoardButtonClicked(int line, int row)
    {
        Debug.Log("On Screen Key Pressed for " + allKeyDefinitionLines[line][row].Item2);
        
        inputFieldCommands.SetTextWithoutNotify(drawStringObject.GetComponentInChildren<DrawStringScript>().SendKey(allKeyDefinitionLines[line][row].Item1));
    }

    // Update is called once per frame
    void Update()
    {
        if (skyBoxMaterialChanged)
        {
            DynamicGI.UpdateEnvironment();
            skyBoxMaterialChanged = false;
        }
        
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.075f);
    }
}
