using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Piper;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public abstract class IScenario : MonoBehaviour
{
    private TextToSpeech tts;
    void Awake()
    {
        tts = FindObjectOfType<TextToSpeech>();
    }

    void AddResponse(string response)
    {
        tts.OnInputSubmit(response);
    }

    abstract public void OnTranscriptionResult(string obj);
}