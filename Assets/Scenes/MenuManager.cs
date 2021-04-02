using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.WindowsDevicePortal;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Application = UnityEngine.Application;

public class MenuManager : MonoBehaviour
{
    public GameObject taskSlateGameObject;

    public GameObject slateTitleGameObject;

    public GameObject slateDescriptionGameObject;

    public GameObject inputFieldGameObject;

    public TMP_InputField tmpInputFieldComponent;

    public GameObject showKeyboardButton;

    private TaskDetails currentOpenTask;

    // private MixedRealityKeyboard mixedRealityKeyboard;
    private TouchScreenKeyboard mixedRealityKeyboard;

    private DateTime sessionStartTime;

    private const string SHOW_KEYBOARD = "Show keyboard";

    private const string HIDE_KEYBOARD = "Hide keyboard";

    private readonly Dictionary<int, TaskDetails> taskIdToTaskDetailsDictionary = new Dictionary<int, TaskDetails>
    {
        {0, new TaskDetails(0, "Task 1", "Task 1 description")},
        {1, new TaskDetails(1, "Task 2", "Task 2 description")},
        {2, new TaskDetails(2, "Task 3", "Task 3 description")},
        {3, new TaskDetails(3, "Task 4", "Task 4 description")},
        {4, new TaskDetails(4, "Task 5", "Task 5 description")},
        {5, new TaskDetails(5, "Task 6", "Task 6 description")},
        {6, new TaskDetails(6, "Task 7", "Task 7 description")},
        {7, new TaskDetails(7, "Task 8", "Task 8 description")},
        {8, new TaskDetails(8, "Task 9", "Task 9 description")}
    };

    public void Start()
    {
        sessionStartTime = DateTime.Now;

        if (!taskSlateGameObject)
        {
            taskSlateGameObject = GameObject.Find("TaskSlate");
        }

        if (!slateTitleGameObject)
        {
            slateTitleGameObject = taskSlateGameObject.transform.Find("TitleBar/Title").gameObject;
        }

        if (!slateDescriptionGameObject)
        {
            slateDescriptionGameObject = taskSlateGameObject.transform.Find("Content/Content/GridLayout/Column/Description").gameObject;
        }

        if (!inputFieldGameObject)
        {
            inputFieldGameObject = taskSlateGameObject.transform.Find("Content/Content/GridLayout/Column/MRKeyboardInputField_TMP").gameObject;
            tmpInputFieldComponent = inputFieldGameObject.GetComponent<TMP_InputField>();
        }

        if (!showKeyboardButton)
        {
            showKeyboardButton = taskSlateGameObject.transform.Find("TitleBar/Buttons/ShowKeyboardButton").gameObject;
        }

        // if (!mixedRealityKeyboard)
        // {
        //     mixedRealityKeyboard = showKeyboardButton.GetComponent<MixedRealityKeyboard>();
        // }
    }

    public void Update()
    {
        if (mixedRealityKeyboard != null)
        {
            tmpInputFieldComponent.SetTextWithoutNotify(mixedRealityKeyboard.text);
        }
    }

    public void ShowTask(int taskId)
    {
        SaveTextForCurrentTask();
        // Null check.
        if (!taskIdToTaskDetailsDictionary.ContainsKey(taskId))
        {
            Debug.LogError("No task defined for taskId: " + taskId);
            return;
        }

        // Sanity check.
        if (currentOpenTask != null && taskSlateGameObject.activeSelf && currentOpenTask.TaskId == taskId)
        {
            Debug.Log("Attempted to open the same task. Not doing anything.");
            return;
        }

        Debug.Log("Showing task slate for taskId: " + taskId);
        currentOpenTask = taskIdToTaskDetailsDictionary[taskId];
        UpdateTaskFieldsWithCurrentTask();
        taskSlateGameObject.SetActive(true);

        // ShowKeyboardForCurrentTask();
    }

    public void UpdateTaskFieldsWithCurrentTask()
    {
        slateTitleGameObject.GetComponent<TextMeshPro>().SetText(currentOpenTask.TaskTitle);
        slateDescriptionGameObject.GetComponent<TextMeshProUGUI>().SetText(currentOpenTask.TaskDescription);
        LoadSavedTextForCurrentTask();
    }

    public void LoadSavedTextForCurrentTask()
    {
        string savedText = "";

        if (File.Exists(GetFilePath()))
        {
            using (StreamReader reader = File.OpenText(GetFilePath()))
            {
                savedText = reader.ReadToEnd();
            }
        }

        // mixedRealityKeyboard.ShowKeyboard(savedText);
        mixedRealityKeyboard = TouchScreenKeyboard.Open(savedText, TouchScreenKeyboardType.Default);
        // tmpInputFieldComponent.SetTextWithoutNotify(savedText);
    }

    public void CloseTask()
    {
        // save
        SaveTextForCurrentTask();

        // Close keyboard
        // keyboard.active = false;
        if (mixedRealityKeyboard.status == TouchScreenKeyboard.Status.Visible)
        {
            // mixedRealityKeyboard.HideKeyboard();
            SetButtonTextOnShowKeyboard();
            mixedRealityKeyboard.text = "";
            // mixedRealityKeyboard.ClearKeyboardText();
        }

        // Close slate.
        taskSlateGameObject.SetActive(false);
    }

    public void SaveTextForCurrentTask()
    {
        if (!taskSlateGameObject.activeSelf)
        {
            Debug.Log("Slate is not active. Not saving the input.");
            return;
        }

        if (currentOpenTask == null)
        {
            Debug.Log("No task is open. Can't save input.");
            return;
        }

        try
        {
            using (TextWriter writer = File.CreateText(GetFilePath()))
            {
                writer.Write(mixedRealityKeyboard.text);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private string GetFilePath()
    {
        string filePath = Path.Combine(Application.persistentDataPath,
            $"{sessionStartTime.ToString("yyyy_MMMM_d__HH_m_s", CultureInfo.InvariantCulture)}_task_{currentOpenTask.TaskId}.txt");
        Debug.Log("File path: " + filePath);
        return filePath;
    }

    public void ToggleKeyboard()
    {
        if (mixedRealityKeyboard.status == TouchScreenKeyboard.Status.Visible)
        {
            // mixedRealityKeyboard.HideKeyboard();
            mixedRealityKeyboard.active = false;
            SetButtonTextOnShowKeyboard();
        }
        else
        {
            // mixedRealityKeyboard.ShowKeyboard();
            mixedRealityKeyboard = TouchScreenKeyboard.Open("");
            SetButtonTextOnHideKeyboard();
        }
    }

    public void SetButtonTextOnShowKeyboard()
    {
        showKeyboardButton.GetComponent<ButtonConfigHelper>().MainLabelText = SHOW_KEYBOARD;
        showKeyboardButton.GetComponent<ButtonConfigHelper>().SeeItSayItLabelText = SHOW_KEYBOARD;
    }

    public void SetButtonTextOnHideKeyboard()
    {
        showKeyboardButton.GetComponent<ButtonConfigHelper>().MainLabelText = HIDE_KEYBOARD;
        showKeyboardButton.GetComponent<ButtonConfigHelper>().SeeItSayItLabelText = HIDE_KEYBOARD;
    }
}