using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private Image _screenLogo;

    [SerializeField]
    private AnimationManager _animationManager;

    [SerializeField]
    private VoskSpeechToText _VoskSpeechToText;

    [SerializeField]
    private TextToSpeech _tts;

    [SerializeField] 
    private GameObject _tgBot;

    private ScenarioFactory _factory = new();

    private Scenario _currentScenario;

    private Scenario _switchScenario;

    private Scenario _mainScenario;

    [SerializeField]
    private TextMeshProUGUI _subtitles;

    private float _fadeDuration = 2.0f;

    private void Awake()
    {
        _VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;

        _switchScenario = _factory.GetScenario("SwitchScenario");
        _switchScenario.OnSay += Say;
        _switchScenario.OnSwitchScenario += SwitchScenario;
        _switchScenario.OnAnimate += Animate;

        _mainScenario = _factory.GetScenario("MainScenario");
        _mainScenario.OnSay += Say;
        _mainScenario.OnAnimate += Animate;
        _mainScenario.NextScenario = _switchScenario;

        _currentScenario = _mainScenario;

        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        _subtitles.gameObject.SetActive(_tts.IsPlaying);
    }

    private void OnTranscriptionResult(string obj)
    {
        if (_tts.IsPlaying)
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
        _tts.OnInputSubmit(response);
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
            _currentScenario.SetTargetObject(_tgBot);

        if (lastPhrase != null)
        {
            _currentScenario.InputProcessing(lastPhrase);
        }
    }

    private void Animate(Animation animation)
    {
        _animationManager.Animate(animation);
    }
    IEnumerator FadeOut()
    {
        // Получаем начальный цвет изображения
        Color startColor = _screenLogo.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Конечный цвет с альфа-каналом 0

        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            // Линейная интерполяция между начальным и конечным цветом
            _screenLogo.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующий кадр
        }

        // Убеждаемся, что цвет установлен в конечное значение
        _screenLogo.color = endColor;
    }
}