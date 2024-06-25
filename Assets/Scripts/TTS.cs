using System;
using UnityEngine;
using UnityEngine.UI;


namespace Piper.Samples
{
    [RequireComponent(typeof(AudioSource))]
    public class PiperSample : MonoBehaviour
    {
        public PiperManager piper;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Update()
        {

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

}

