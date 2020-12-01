﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.DataObjects;

public class StartUpInitialization : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas canvasObject;
    public Dropdown dropdown;
    public InputField inputFieldTitle;
    public InputField inputFieldCommands;
    public Button clearButton;
    public Button saveButton;
    public Button pauseButton;
    public Slider speedSlider;
    public Slider fieldOfViewSlider;
    public Toggle showControlsToggle;
    public bool animationPaused = false;
    public GameObject DrawStringObject;
    private DynaDrawOriginalCreations dynaDrawOriginalCreations;
    private DynaDrawSavedCreations dynaDrawSavedCreations;
    private int currentDropdownSelectionIndex = 0;
    private int savedCursorPosition = 0;

    private void Awake()
    {
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
        speedSlider.onValueChanged.AddListener(delegate { SpeedSliderChanged(speedSlider.value); });
        fieldOfViewSlider.onValueChanged.AddListener(delegate { FieldOfViewSliderChanged(fieldOfViewSlider.value); });
        showControlsToggle.onValueChanged.AddListener(delegate { ShowControlsToggleChanged(showControlsToggle.isOn); });
    }

    void TextEditTitleValueChanged()
    {
        saveButton.GetComponentInChildren<Text>().text = "Save";
        saveButton.interactable = true;
    }

    void TextEditCommandsValueChanged()
    {
        if (currentDropdownSelectionIndex < dynaDrawOriginalCreations.OriginalCreationsList.Count) //Editing an Original Creation -> potentially save it as new       
            saveButton.GetComponentInChildren<Text>().text = "Save";            
        if (saveButton.GetComponentInChildren<Text>().text != "Save")  // if title was changed, this will still be a save
            saveButton.GetComponentInChildren<Text>().text = "Update";
        saveButton.interactable = true;
    }

    void DropDownValueChanged(int index)
    {
        var originalCreationsListSize = dynaDrawOriginalCreations.OriginalCreationsList.Count;
        currentDropdownSelectionIndex = index;
        if (index == 0)
        {
            inputFieldCommands.text = "";
            inputFieldTitle.text = "";
            saveButton.GetComponentInChildren<Text>().text = "Save";
            saveButton.interactable = false;
            return;
        }
        if (index < originalCreationsListSize)
        {
            inputFieldCommands.text = dynaDrawOriginalCreations.OriginalCreationsList[index].DynaDrawCommands;
            inputFieldTitle.text = dynaDrawOriginalCreations.OriginalCreationsList[index].Title;
            saveButton.GetComponentInChildren<Text>().text = "Delete";
            saveButton.interactable = false;
            return;
        }
        if (index > originalCreationsListSize)
        {
            inputFieldCommands.text = dynaDrawSavedCreations.UserSaveCreationsList[index - originalCreationsListSize - 1].DynaDrawCommands;
            inputFieldTitle.text = dynaDrawSavedCreations.UserSaveCreationsList[index - originalCreationsListSize - 1].Title;
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
    }

    void FieldOfViewSliderChanged(float newFieldOfViewValue)
    {
        mainCamera.fieldOfView = newFieldOfViewValue;
    }

    void ClearButtonClicked()
    {
        inputFieldCommands.text = "";
        inputFieldTitle.text = "";
        saveButton.GetComponentInChildren<Text>().text = "Save";
        saveButton.interactable = false;
        dropdown.value = 0;
        currentDropdownSelectionIndex = 0;
    }
    
    void SaveButtonClicked()
    {
        if (saveButton.GetComponentInChildren<Text>().text == "Save")
        {
            var title = "Auto Generated Title";
            if (inputFieldTitle.text.Length != 0)
                title = inputFieldTitle.text;

            dynaDrawSavedCreations.Add(title, inputFieldCommands.text);
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

            dynaDrawSavedCreations.Update(currentDropdownSelectionIndex - dynaDrawOriginalCreations.OriginalCreationsList.Count - 1, title, inputFieldCommands.text);
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

    void PauseButtonClicked()
    {
        if (animationPaused)
        {
            if (speedSlider.value == 0) // If we un-pause and the slider is zero, set it to 1
                speedSlider.value = 1;
            Time.timeScale = speedSlider.value;                     
            pauseButton.GetComponentInChildren<Text>().text = "Pause";
        }
        else
        {                        
            Time.timeScale = 0;
            pauseButton.GetComponentInChildren<Text>().text = "Run";
        }
        animationPaused = !animationPaused;
    }

    void ShowControlsToggleChanged(bool toggleIsOn)
    {
        var drawStringScript = DrawStringObject.GetComponentInChildren<DrawStringScript>();
        for (int i = 0; i < canvasObject.transform.childCount - 1; i++ )
        {
            var go = canvasObject.transform.GetChild(i).gameObject;
            if (go.transform.tag == "No Hide")
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
            

    }

    // Start is called before the first frame update
    void Start()
    {
        dynaDrawOriginalCreations = new DynaDrawOriginalCreations();
        dynaDrawSavedCreations = new DynaDrawSavedCreations();
        dynaDrawSavedCreations.GetFromJson();

        PopulateDropDown();    
    }

    void PopulateDropDown()
    {
        dropdown.AddOptions(dynaDrawOriginalCreations.JustTitles());
        dropdown.AddOptions(new List<string>(){ "----Your Creations Below----"});
        dropdown.AddOptions(dynaDrawSavedCreations.JustTitles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
