using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class SwitchScenario : Scenario
{
    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex("@���|������ ���"), "MoveScenario" },
    };

    public override void InputProcessing(string inputPhrase)
    {
        Debug.Log("InputProcessing in switch");
        foreach (var line in dialog)
        {
            if (line.Key.IsMatch(inputPhrase))
            {
                OnSwitchScenario?.Invoke(line.Value, inputPhrase);
                return;
            }
        }
        OnSay?.Invoke("� ���� �� �������");
        OnAnimate?.Invoke(Animation.HEADNO);
    }
}