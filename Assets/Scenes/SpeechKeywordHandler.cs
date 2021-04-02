using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class SpeechKeywordHandler : MonoBehaviour, IMixedRealitySpeechHandler
{
    // Start is called before the first frame update
    void Start()
    {
        //CoreServices.InputSystem?.RegisterHandler<IMixedRealitySpeechHandler>(this);
    }

    void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySpeechHandler>(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Add2DView()
    {
        Debug.Log("Add command received.");
    }

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        if (eventData.Command.Keyword == "smaller")
        {
            transform.localScale *= 0.5f;
        }
        else if (eventData.Command.Keyword == "bigger")
        {
            transform.localScale *= 2.0f;
        }
        else if (eventData.Command.Keyword == "add")
        {
            Debug.Log("Add");
        }
    }
}
