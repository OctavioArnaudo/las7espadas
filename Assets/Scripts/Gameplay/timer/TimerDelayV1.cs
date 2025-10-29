using UnityEngine;

public class TimerDelayV1 : MonoModular
{
    private float _delayTime;
    public float delayTime
    {
        get => _delayTime;
        set => _delayTime = value;
    }

    private float _startTime;
    public float startTime
    {
        get => _startTime;
        set => _startTime = value;
    }

    private bool _isWaiting;
    public bool isWaiting
    {
        get => _isWaiting;
        set => _isWaiting = value;
    }

    private bool _isDone;
    public bool isDone
    {
        get => _isDone;
        set => _isDone = value;
    }


    public TimerDelayV1(float delaySeconds)
    {
        this.delayTime = delaySeconds;
        this.isWaiting = false;
    }

    public virtual void OnStart()
    {
        startTime = Time.time;
        isWaiting = true;
    }

    public virtual void OnExit()
    {
        if (!isWaiting) isDone = true;
        if (Time.time - startTime >= delayTime)
        {
            isWaiting = false;
            isDone = true;
        }
        isDone = false;
    }
}
