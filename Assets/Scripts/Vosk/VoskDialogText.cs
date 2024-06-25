using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Piper.Samples;

public class VoskDialogText : MonoBehaviour 
{
	[SerializeField]
	private MovePlayer player;

    public VoskSpeechToText VoskSpeechToText;
    public Text DialogText;

    public PiperSample tts;

    Regex left_regex = new Regex(@"влево|лево");
	Regex right_regex = new Regex(@"вправо|право");
	Regex up_regex = new Regex(@"вверх|верх");
	Regex down_regex = new Regex(@"вниз|низ");
    Regex hello_regex = new Regex(@"привет");
    Regex howAreYou_regex = new Regex(@"как дела");
    Regex irina_regex = new Regex(@"ирина|рина");

    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
    }

	void AddResponse(string response) {
        tts.OnInputSubmit(response);
        DialogText.text = response + "\n\n";
	}

    private void OnTranscriptionResult(string obj)
    {
        Debug.Log("Сказал: " + obj);
        var result = new RecognitionResult(obj);

        foreach (RecognizedPhrase p in result.Phrases)
        {
            if (left_regex.IsMatch(p.Text))
            {
                player.moveLeft();
                AddResponse("Движение влево");
                return;
            }
            if (right_regex.IsMatch(p.Text))
            {
                player.moveRight();
                AddResponse("Движение вправо");
                return;
            }
            if (up_regex.IsMatch(p.Text))
            {
                player.moveUp();
                AddResponse("Движение вверх");
                return;
            }
            if (down_regex.IsMatch(p.Text))
            {
                player.moveDown();
                AddResponse("Движение вниз");
                return;
            }
            if (hello_regex.IsMatch(p.Text))
            {
                AddResponse("Привет, меня зовут Ирина");
                return;
            }
            if (irina_regex.IsMatch(p.Text))
            {
                AddResponse("Да чё опять");
                return;
            }
            if (howAreYou_regex.IsMatch(p.Text))
            {
                AddResponse("У меня все хорошо, а у тебя?");
                return;
            }
        }
		if (result.Phrases.Length > 0 && result.Phrases[0].Text != "") {
			AddResponse("я тебя не понимаю");
		}
    }
}
