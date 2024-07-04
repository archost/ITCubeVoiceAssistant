using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MainScenario : Scenario
{
    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex(@"������"), "������, ��� ��������� ��������" },
        { new Regex(@"��� ����"), "� ���� ��� ������, � � ���?" },
        { new Regex(@"����� ������ ������|������"), "������ � ����, � �� ���� ������������" },
    };

    public override void OnTranscriptionResult(string obj) 
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
    }
}
