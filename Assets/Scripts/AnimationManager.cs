using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private GameObject _robot;

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
}
