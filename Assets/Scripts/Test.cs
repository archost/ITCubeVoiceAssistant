using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Action a;

    private void Start()
    {
        a = SayHello;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            a.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            a = SayGoodbye;
        }
    }

    private void SayHello()
    {
        Debug.Log("hello");
    }

    private void SayGoodbye()
    {
        Debug.Log("goodbye");
    }
}
