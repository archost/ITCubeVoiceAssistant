using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private GameObject _robot;

    private Dictionary<Animation, string> _animationTable;

    private AnimationManager()
    {
        _animationTable = new Dictionary<Animation, string>() 
        {
            { Animation.WAVING, nameof(Waving) },
            { Animation.HEADNO, nameof(HeadNo) },
        };
    }

    void Start()
    {
        _animator = _robot.GetComponent<Animator>();
    }

    public IEnumerator Waving()
    {
        _animator.SetBool("WavingTransition", true);
        yield return new WaitForSeconds(2);
        _animator.SetBool("WavingTransition", false);
    }

    public IEnumerator HeadNo()
    {
        _animator.SetBool("HeadNoTransition", true);
        yield return new WaitForSeconds(1);
        _animator.SetBool("HeadNoTransition", false);
    }

    public void Animate(Animation animation)
    {
        if (_animationTable.ContainsKey(animation))
        {
            StartCoroutine(_animationTable[animation]);
        }
    }
}

public enum Animation
{
    IDLE,
    WAVING,
    HEADNO,

}