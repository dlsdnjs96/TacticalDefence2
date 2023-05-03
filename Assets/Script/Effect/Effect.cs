using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }



    public void PlayOnce()
    {
        animator.SetTrigger("Play");
    }
    public void PlayRepeat(float _duration, int _time)
    {
        StartCoroutine(Repeat(_duration, _time));
    }
    public void PlayLoop(float _duration)
    {
        StartCoroutine(Loop(_duration));
    }
    public void Stop()
    {
        StopCoroutine(Loop(0f));
        StopCoroutine(Repeat(0f, 0));
    }

    IEnumerator Loop(float _duration)
    {
        animator.SetTrigger("Play");
        StartCoroutine(Loop(_duration));

        yield return new WaitForSeconds(_duration);
    }

    IEnumerator Repeat(float _duration, int _time)
    {
        animator.SetTrigger("Play");
        if (_time <= 0)
            StartCoroutine(Repeat(_duration, _time - 1));

        yield return new WaitForSeconds(_duration);
    }
}
