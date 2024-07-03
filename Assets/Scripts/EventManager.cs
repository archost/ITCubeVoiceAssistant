using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public VoskSpeechToText VoskSpeechToText;

    public TextToSpeech tts;

    [SerializeField]
    private List<GameObject> prefabs = new List<GameObject>();

    private GameObject _currentScenario = null;

    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex(@"привет"), "Привет, меня зовут Ирина" }
    };

    private void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += GlobalOnTranscriptionResult;
    }

    void AddResponse(string response)
    {
        tts.OnInputSubmit(response);
    }

    private void GlobalOnTranscriptionResult(string obj)
    {
        var result = new RecognitionResult(obj);

        string phrase = result.Phrases[0].Text;

        foreach (var item in dialog)
        {
            if (item.Key.IsMatch(phrase))
            {
                AddResponse(item.Value);
                ActivateScenario(prefabs[0]);
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