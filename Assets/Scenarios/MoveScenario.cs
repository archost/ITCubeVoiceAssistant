using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MoveScenario : Scenario
{
    private MovePlayer _cube;

    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex("@������|�����"), "�������� ������" },
        { new Regex(@"�����|����"), "�������� �����" },
        { new Regex(@"�����|����"), "�������� �����" },
        { new Regex(@"����|���"), "�������� ����" },
        { new Regex(@"�������"), "������ ���� ������" },
    };

    private void Start()
    {
        _cube = FindAnyObjectByType<MovePlayer>();
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
                Move(item.Value);
                return;
            }
        }
    }

    private void Move(string direction)
    {
        switch (direction)
        {
            case ("�������� ������"):
                _cube.moveRight();
                return;
            case ("�������� �����"):
                _cube.moveLeft();
                return;
            case ("�������� �����"):
                _cube.moveUp();
                return;
            case ("�������� ����"):
                _cube.moveDown();
                return;
            default:
                return;
        }
    }
}
