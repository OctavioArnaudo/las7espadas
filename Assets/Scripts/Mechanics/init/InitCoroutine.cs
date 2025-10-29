using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class InitCoroutine : InitCanvas
{

    [SerializeField] protected Coroutine coroutine;
    
    protected Coroutine RestartCoroutine(IEnumerator method)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(method);
        return coroutine;
    }

    protected IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}