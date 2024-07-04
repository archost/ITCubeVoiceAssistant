using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MoveScenario : Scenario
{
    private MovePlayer _cube;

    private Dictionary<Regex, string> dialog = new Dictionary<Regex, string>
    {
        { new Regex("@вправо|право"), "Движение вправо" },
        { new Regex(@"влево|лево"), "Движение влево" },
        { new Regex(@"вверх|верх"), "Движение вверх" },
        { new Regex(@"вниз|низ"), "Движение вниз" },
        { new Regex(@"спасибо"), "Всегда рада помочь" },
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
}
