using System;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class SerializableRegex
{
    [SerializeField]
    private string pattern;

    public Regex Regex
    {
        get
        {
            return new Regex(pattern);
        }
        set
        {
            pattern = value.ToString();
        }
    }

    public SerializableRegex(string pattern)
    {
        this.pattern = pattern;
    }

    public SerializableRegex(Regex regex)
    {
        this.pattern = regex.ToString();
    }
}