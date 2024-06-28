using Piper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToSpeech : MonoBehaviour
{
    public PiperManager piper;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public async void OnInputSubmit(string text)
    {
        var audio = piper.TextToSpeech(text);

        _source.Stop();
        if (_source && _source.clip)
            Destroy(_source.clip);

        _source.clip = await audio;
        _source.Play();
    }

    private void OnDestroy()
    {
        if (_source && _source.clip)
            Destroy(_source.clip);
    }
}
