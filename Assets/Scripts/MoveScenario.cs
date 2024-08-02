using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MoveScenario : Scenario
{
    //private MovePlayer _cube;

    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex("@���|������ ���"), "������, ����" },
        { new Regex("@������|�����"), "�������� ������" },
        { new Regex(@"�����|����"), "�������� �����" },
        { new Regex(@"�����|����"), "�������� �����" },
        { new Regex(@"����|���"), "�������� ����" },
        { new Regex(@"�������"), "������ ���� ������" },
    };

    private void Start()
    {
    //    _cube = FindAnyObjectByType<MovePlayer>();
    }

    public override void InputProcessing(string inputPhrase)
    {
        foreach (var line in dialog)
        {
            if (line.Key.IsMatch(inputPhrase))
            {
                OnSay?.Invoke(line.Value);
                // Move(line.Value);
                return;
            }
        }
        if (NextScenario != null)
        {
            NextScenario.InputProcessing(inputPhrase);
        }
    }
    /*
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
    */
}