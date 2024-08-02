using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MoveScenario : Scenario
{
    //private MovePlayer _cube;

    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex("@куб|двигай куб"), "Хорошо, куда" },
        { new Regex("@вправо|право"), "Движение вправо" },
        { new Regex(@"влево|лево"), "Движение влево" },
        { new Regex(@"вверх|верх"), "Движение вверх" },
        { new Regex(@"вниз|низ"), "Движение вниз" },
        { new Regex(@"спасибо"), "Всегда рада помочь" },
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
            case ("Движение вправо"):
                _cube.moveRight();
                return;
            case ("Движение влево"):
                _cube.moveLeft();
                return;
            case ("Движение вверх"):
                _cube.moveUp();
                return;
            case ("Движение вниз"):
                _cube.moveDown();
                return;
            default:
                return;
        }
    }
    */
}