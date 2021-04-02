using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDetails
{
    private int taskId;
    private string taskTitle = "Task";
    private string taskDescription = "Task description";
    // private bool shouldActivateKeyboard = true;
    // private TouchScreenKeyboardType touchScreenKeyboardType = TouchScreenKeyboardType.Default;
    // private bool shouldEnableKeyboardAutocorrect = false;
    // private bool shouldKeyboardAcceptMultilineText = false;
    // private string placeholderText;
    // private int characterLimit = 0;

    public TaskDetails(int taskId, string taskTitle, string taskDescription)
    {
        this.taskId = taskId;
        this.taskTitle = taskTitle;
        this.taskDescription = taskDescription;
    }

    // public TaskDetails(int taskId, string taskTitle, string taskDescription, bool shouldActivateKeyboard, TouchScreenKeyboardType touchScreenKeyboardType,
    //     bool shouldEnableKeyboardAutocorrect, bool shouldKeyboardAcceptMultilineText, string placeholderText, int characterLimit)
    // {
    //     this.taskId = taskId;
    //     this.taskTitle = taskTitle;
    //     this.taskDescription = taskDescription;
    //     this.shouldActivateKeyboard = shouldActivateKeyboard;
    //     this.touchScreenKeyboardType = touchScreenKeyboardType;
    //     this.shouldEnableKeyboardAutocorrect = shouldEnableKeyboardAutocorrect;
    //     this.shouldKeyboardAcceptMultilineText = shouldKeyboardAcceptMultilineText;
    //     this.placeholderText = placeholderText;
    //     this.characterLimit = characterLimit;
    // }

    public int TaskId
    {
        get => taskId;
        set => taskId = value;
    }

    public string TaskTitle
    {
        get => taskTitle;
        set => taskTitle = value;
    }

    public string TaskDescription
    {
        get => taskDescription;
        set => taskDescription = value;
    }

    // public TouchScreenKeyboardType TouchScreenKeyboardType
    // {
    //     get => touchScreenKeyboardType;
    //     set => touchScreenKeyboardType = value;
    // }
    //
    // public bool ShouldActivateKeyboard
    // {
    //     get => shouldActivateKeyboard;
    //     set => shouldActivateKeyboard = value;
    // }
    //
    // public bool ShouldEnableKeyboardAutocorrect
    // {
    //     get => shouldEnableKeyboardAutocorrect;
    //     set => shouldEnableKeyboardAutocorrect = value;
    // }
    //
    // public bool ShouldKeyboardAcceptMultilineText
    // {
    //     get => shouldKeyboardAcceptMultilineText;
    //     set => shouldKeyboardAcceptMultilineText = value;
    // }
    //
    // public string PlaceholderText
    // {
    //     get => placeholderText;
    //     set => placeholderText = value;
    // }
    //
    // public int CharacterLimit
    // {
    //     get => characterLimit;
    //     set => characterLimit = value;
    // }
}