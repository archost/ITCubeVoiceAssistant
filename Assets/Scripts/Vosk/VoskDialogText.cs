using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Piper;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class VoskDialogText : MonoBehaviour
{
    private TextToSpeech tts;

    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex("@Двигай куб|куб"), "Я не могу двигать куб" },
        { new Regex(@"привет"), "Привет, меня зовут Ирина" },
        { new Regex(@"как дела"), "У меня все хорошо, а у вас?" },
        { new Regex(@"ирина|рина"), "Да чё опять" },
    };
    
    void Awake()
    {
        tts = FindObjectOfType<TextToSpeech>();
        // VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
    }
    
	void AddResponse(string response) {
        tts.OnInputSubmit(response);
	}

    public void OnTranscriptionResult(string obj)
    {
        var result = new RecognitionResult(obj);

        string phrase = result.Phrases[0].Text;

        foreach (var item in dialog)
        {
            if (item.Key.IsMatch(phrase))
            {
                AddResponse(item.Value);
                return;
            }
        }

        if (result.Phrases.Length > 0 && result.Phrases[0].Text != "") {
			AddResponse("Я тебя не понимаю");
            return;
		}
    }
}