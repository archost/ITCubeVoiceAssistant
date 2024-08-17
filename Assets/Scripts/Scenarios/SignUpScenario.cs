using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class SignUpScenario : Scenario
{
    private Dictionary<Regex, Action> dialog;

    private State _currentState;

    public TelegramBot tgBot;

    private string _name;

    private Dictionary<string, string> wordToDigit = new Dictionary<string, string>
    {
        { "ноль", "0" }, { "один", "1" }, { "два", "2" }, { "три", "3" }, { "четыре", "4" },
        { "пять", "5" }, { "шесть", "6" }, { "семь", "7" }, { "восемь", "8" }, { "девять", "9" }
    };

    public SignUpScenario()
    {
        _currentState = State.IDLE;
        dialog = new Dictionary<Regex, Action>
        {
            { new Regex(@"записаться"), () => {
                OnSay?.Invoke("Вы хотите записаться в айти куб? Замечательно, скажите, как вас зовут?");
                _currentState = State.WAITINGFORNAME;
                }
            },
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
        
        if (_currentState == State.WAITINGFORNAME)
        {
            _name = inputPhrase;
            _currentState = State.WAITINGFORNUMBER;
            OnSay?.Invoke("Хорошо, продиктуйте свой номер телефона. Отдельно каждую цифру");
            return;
        }
        else if (_currentState == State.WAITINGFORNUMBER)
        {
            string number = WordsToDigits(inputPhrase, out bool successful);
            if (!successful)
            {
                OnSay?.Invoke("Пожалуйста, продиктуйте все цифры номера отдельно");
                return;
            }
            getBot();
            string message = "Заявка на запись от " + DateTime.Now.ToString("dd.MM.yy\n");
            message += "Имя: " + _name + "\n";
            message += "Номер: " + number + "\n";
            message += "(" + inputPhrase + ")";
            tgBot.SendMsg(message);
            OnSay?.Invoke("Спасибо за обращение");
            _currentState = State.IDLE;
            _name = "";
            return;
        }
        else
        {
            _name = "";
            _currentState = State.IDLE;
        }

        if (NextScenario != null && inputPhrase != "")
        {
            NextScenario.InputProcessing(inputPhrase);
        }
    }

    private void getBot()
    {
        tgBot = targetObject.GetComponent<TelegramBot>();
    }

    private string WordsToDigits(string phoneWords, out bool successful)
    {
        successful = true;
        string[] words = Regex.Matches(phoneWords.ToLower(), @"\b\w+\b")
                              .Cast<Match>()
                              .Select(m => m.Value)
                              .ToArray();

        List<string> digitsList = new List<string>();
        int i = 0;
        while (i < words.Length)
        {
            string word = words[i];
            if (wordToDigit.ContainsKey(word))
            {
                digitsList.Add(wordToDigit[word]);
                i++;
            }
            else
            {
                successful = false;
                break;
            }
        }
        string digits = string.Join("", digitsList);
        return digits;
    }
}

public enum State
{
    IDLE,
    WAITINGFORNAME,
    WAITINGFORNUMBER,
}