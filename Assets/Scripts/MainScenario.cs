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
            { new Regex(@"������"), () => { OnSay?.Invoke("������, ��� ��������� ��������"); OnAnimate?.Invoke(Animation.WAVING); } },
            { new Regex(@"��� ����"), () => { OnSay?.Invoke("� ���� ��� ������, � � ���?"); } },
            { new Regex(@"����� ������ ������|������"), () => { OnSay?.Invoke("������ � ����, � �� ���� ������������"); } },
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