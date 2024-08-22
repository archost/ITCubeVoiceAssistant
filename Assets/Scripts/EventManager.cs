using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public AnimationManager animationManager;

    public VoskSpeechToText VoskSpeechToText;

    public TextToSpeech tts;

    public GameObject tgBot;

    private ScenarioFactory _factory = new();

    private Scenario _currentScenario;

    private Scenario _switchScenario;

    private Scenario _mainScenario;

    [SerializeField]
    private TextMeshProUGUI _subtitles;

    private void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;

        _switchScenario = _factory.GetScenario("SwitchScenario");
        _switchScenario.OnSay += Say;
        _switchScenario.OnSwitchScenario += SwitchScenario;
        _switchScenario.OnAnimate += Animate;

        _mainScenario = _factory.GetScenario("MainScenario");
        _mainScenario.OnSay += Say;
        _mainScenario.OnAnimate += Animate;
        _mainScenario.NextScenario = _switchScenario;

        _currentScenario = _mainScenario;
    }

    private void Update()
    {
        _subtitles.gameObject.SetActive(tts.IsPlaying);
    }

    private void OnTranscriptionResult(string obj)
    {
        if (tts.IsPlaying)
            return;
        var result = new RecognitionResult(obj);
        string phrase = result.Phrases[0].Text;

        Debug.Log("Input: " + phrase);
        if (phrase != "")
            _currentScenario.InputProcessing(phrase);
    }

    private void Say(string response)
    {
        _subtitles.text = response;
        tts.OnInputSubmit(response);
    }

    private void SwitchScenario(string newScenario, string lastPhrase = null)
    {
        if (_currentScenario != null && _currentScenario != _mainScenario)
        {
            _currentScenario.OnSay -= Say;
            _currentScenario.OnAnimate -= Animate;
        }

        _currentScenario = _factory.GetScenario(newScenario);
        _currentScenario.OnSay += Say;
        _currentScenario.OnAnimate += Animate;

        _currentScenario.NextScenario = _mainScenario;

        if (newScenario == "SignUpScenario")
            _currentScenario.SetTargetObject(tgBot);

        if (lastPhrase != null)
        {
            _currentScenario.InputProcessing(lastPhrase);
        }
    }

    private void Animate(Animation animation)
    {
        animationManager.Animate(animation);
    }


}