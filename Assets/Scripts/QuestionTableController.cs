using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class QuestionTableController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _questionsText;

    private List<String> _questions = new List<String>() { "Расскажи про IT-куб", "Какие есть направления?", 
        "Программирование на питоне", "Курс робототехника", "Малая комьютерная академия", 
        "Что такое алгоритмика?", "Курс 3D моделирование", "Курс кибербезопасность", "Хочу записаться на учебу"};

    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            _questionsText.text += _questions[i] + "\n\n";
        }

        StartCoroutine(ChangeQuestions());
    }

    private IEnumerator ChangeQuestions()
    {
        yield return new WaitForSeconds(30);

        _questionsText.text = "";

        List<int> added = new();

        for (int i = 0; i < 9; i++)
        {
            int currentId = UnityEngine.Random.Range(0, _questions.Count);
            while (added.Contains(currentId))
            {
                currentId = UnityEngine.Random.Range(0, _questions.Count);
            }
            added.Add(currentId);
            _questionsText.text += _questions[currentId] + "\n\n";
        }

        yield return StartCoroutine(ChangeQuestions());
    }
}
