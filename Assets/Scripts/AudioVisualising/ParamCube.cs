using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParamCube : MonoBehaviour
{
    [SerializeField]
    private int _band;

    [SerializeField]
    private float _startScale, _scaleMultiplier;

    [SerializeField]
    private bool _useBuffer;

    private float _scaleY;

    void Update()
    {
        if (_useBuffer)
            _scaleY = AudioVisualizer._bandBuffer[_band] * _scaleMultiplier + _startScale;
        else
            _scaleY = AudioVisualizer._freqBand[_band] * _scaleMultiplier + _startScale;
        transform.localScale = new Vector3(transform.localScale.x, _scaleY, transform.localScale.z);
    }
}