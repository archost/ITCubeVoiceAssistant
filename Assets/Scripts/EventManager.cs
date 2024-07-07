using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public VoskSpeechToText VoskSpeechToText;

    public TextToSpeech tts;

    [SerializeField]
    private List<SerializableKeyValuePair<SerializableKeyValuePair<SerializableRegex, string>, GameObject>> table = 
        new List<SerializableKeyValuePair<SerializableKeyValuePair<SerializableRegex, string>, GameObject>>();

    private GameObject _currentScenario = null;

    private AnimationManager _animManager;

    private void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += GlobalOnTranscriptionResult;
        _animManager = FindObjectOfType<AnimationManager>();
    }

    void AddResponse(string response)
    {
        tts.OnInputSubmit(response);
    }

    private void GlobalOnTranscriptionResult(string obj)
    {
        var result = new RecognitionResult(obj);
        string phrase = result.Phrases[0].Text;
        Debug.Log(phrase);
        foreach (var item in table)
        {
            var pair = item.ToPair();
            var regex_response = pair.Key.ToPair();
            if (regex_response.Key.Regex.IsMatch(phrase))
            {
                AddResponse(regex_response.Value);
                ActivateScenario(pair.Value);
                if (regex_response.Key.Regex.IsMatch("привет"))
                    StartCoroutine(_animManager.Waving());
                return;
            }
        }
    }

    private void ActivateScenario(GameObject obj)
    {
        if (_currentScenario != null)
        {
            Scenario script2 = _currentScenario.GetComponent<Scenario>();
            VoskSpeechToText.OnTranscriptionResult -= script2.OnTranscriptionResult;
            Destroy(_currentScenario);
        }
            
        _currentScenario = Instantiate(obj);
        Scenario script = _currentScenario.GetComponent<Scenario>();
        VoskSpeechToText.OnTranscriptionResult += script.OnTranscriptionResult;
    }
}