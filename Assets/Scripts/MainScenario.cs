using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class MainScenario : Scenario
{
    private Dictionary<Regex, Action> dialog;

    public MainScenario() 
    {
        dialog = new Dictionary<Regex, Action>
        {
            { new Regex(@"привет"), () => { OnSay?.Invoke("Привет, это голосовой помощник"); OnAnimate?.Invoke(Animation.WAVING); } },
            { new Regex(@"как дела"), () => { OnSay?.Invoke("У меня все хорошо, а у вас?"); } },
            { new Regex(@"какая сейчас погода|погода"), () => { OnSay?.Invoke("Откуда я знаю, я чё тебе метеостанция"); } },
        };
    }

    public override void InputProcessing(string inputPhrase)
    {
        foreach (var line in dialog)
        {
            if (line.Key.IsMatch(inputPhrase))
            {
                line.Value?.Invoke();
                return;
            }
        }
        if (NextScenario != null && inputPhrase != "")
        {
            NextScenario.InputProcessing(inputPhrase);
        }
    }
}