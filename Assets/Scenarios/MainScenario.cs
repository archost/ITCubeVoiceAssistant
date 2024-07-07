using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MainScenario : Scenario
{
    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex(@"привет"), "Привет, это голосовой помощник" },
        { new Regex(@"как дела"), "У меня все хорошо, а у вас?" },
        { new Regex(@"какая сейчас погода|погода"), "Откуда я знаю, я чё тебе метеостанция" },
    };

    private AnimationManager _animManager;

    private void Start()
    {
        _animManager = FindObjectOfType<AnimationManager>();
    }

    public override void OnTranscriptionResult(string obj) 
    {
        var result = new RecognitionResult(obj);

        string phrase = result.Phrases[0].Text;

        foreach (var item in dialog)
        {
            if (item.Key.IsMatch(phrase))
            {
                AddResponse(item.Value);
                if (item.Key.IsMatch("привет"))
                    StartCoroutine(_animManager.Waving());
                return;
            }
        }
    }
}
