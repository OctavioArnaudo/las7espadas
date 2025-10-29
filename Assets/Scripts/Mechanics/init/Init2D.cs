using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Init2D : BaseTrailRenderer
{
    public static List<Func<GameObject, GameObject>> OnGlobalObjectDetecting = new();
    protected List<Func<GameObject, GameObject>> OnObjectDetecting = new();
    public static List<Func<GameObject, GameObject>> OnGlobalDetecting = new();
    protected List<Func<GameObject, GameObject>> OnDetecting = new();

    public static event Func<GameObject, GameObject> OnGlobalObjectDetected;
    protected event Func<GameObject, GameObject> OnObjectDetected;
    public static Func<GameObject, GameObject> OnGlobalDetected => null;
    protected virtual Func<GameObject, GameObject> OnDetected => null;

    public static UnityEvent<GameObject> OnGlobalObjectInspected = new();
    protected UnityEvent<GameObject> OnObjectInspected = new();
    public static event Action<GameObject> OnGlobalObjectProcessed;
    protected event Action<GameObject> OnObjectProcessed;
    public static UnityEvent<GameObject> OnGlobalInspected => null;
    protected virtual UnityEvent<GameObject> OnInspected => null;
    public static Action<GameObject> OnGlobalProcessed => null;
    protected virtual Action<GameObject> OnProcessed => null;

    public IEnumerator Add<T>(List<Func<T, T>> e, List<Func<T, T>> m)
    {
        e.AddRange(m);
        yield return null;
    }
    public IEnumerator Add<T>(List<Func<T, T>> e, Func<T, T> m)
    {
        e.Add(m);
        yield return null;
    }
    public IEnumerator Add<T>(Func<T, T> e, Func<T, T> m)
    {
        e += m;
        yield return null;
    }
    public IEnumerator Add<T>(Action<T> e, Action<T> m)
    {
        e += m;
        yield return null;
    }
    public IEnumerator Add<T>(UnityEvent<T> e, UnityAction<T> m)
    {
        e.AddListener(m);
        yield return null;
    }

    public IEnumerator Remove<T>(List<Func<T, T>> e, List<Func<T, T>> m)
    {
        e.Remove(m.Last());
        yield return null;
    }
    public IEnumerator Remove<T>(List<Func<T, T>> e, Func<T, T> m)
    {
        e.Remove(m);
        yield return null;
    }
    public IEnumerator Remove<T>(Func<T, T> e, Func<T, T> m)
    {
        e -= m;
        yield return null;
    }
    public IEnumerator Remove<T>(Action<T> e, Action<T> m)
    {
        e -= m;
        yield return null;
    }
    public IEnumerator Remove<T>(UnityEvent<T> e, UnityAction<T> m)
    {
        e.RemoveListener(m);
        yield return null;
    }

    public IEnumerator Clear()
    {
        OnGlobalObjectDetecting.Clear();
        OnGlobalDetecting.Clear();
        OnGlobalObjectDetected = null;
        OnGlobalObjectProcessed = null;
        OnGlobalObjectInspected.RemoveAllListeners();
        OnGlobalInspected.RemoveAllListeners();
        yield return null;
    }
    protected IEnumerator RemoveAllListeners()
    {
        OnObjectDetecting.Clear();
        OnDetecting.Clear();
        OnObjectDetected = null;
        OnObjectProcessed = null;
        OnObjectInspected.RemoveAllListeners();
        OnInspected.RemoveAllListeners();
        yield return null;
    }

    public static bool GlobalAutoSubscribe => true;
    protected virtual bool AutoSubscribe => true;

    public override void OnStart()
    {
        base.OnStart();
        if (OnGlobalDetecting != null && GlobalAutoSubscribe)
        {
            Add(OnGlobalObjectDetecting, OnGlobalDetecting);
        }
        if (OnDetecting != null && AutoSubscribe)
        {
            Add(OnObjectDetecting, OnDetecting);
        }

        if (GlobalAutoSubscribe && OnDetecting.Count > 0)
        {
            foreach (var func in OnDetecting)
            {
                if (!OnGlobalObjectDetecting.Contains(func))
                    Add(OnGlobalObjectDetecting, func);
            }
        }
        if (AutoSubscribe && OnGlobalDetecting.Count > 0)
        {
            foreach (var func in OnGlobalDetecting)
            {
                if (!OnObjectDetecting.Contains(func))
                    Add(OnObjectDetecting, func);
            }
        }

        if (OnGlobalDetected != null && GlobalAutoSubscribe)
        {
            Add(OnGlobalObjectDetected, OnGlobalDetected);
        }
        if (OnDetected != null && AutoSubscribe)
        {
            Add(OnObjectDetected, OnDetected);
        }

        if (OnGlobalDetected != null && AutoSubscribe)
        {
            Add(OnObjectDetected, OnGlobalDetected);
        }
        if (OnDetected != null && GlobalAutoSubscribe)
        {
            Add(OnGlobalObjectDetected, OnDetected);
        }

        if (OnGlobalProcessed != null && GlobalAutoSubscribe)
        {
            Add(OnGlobalObjectProcessed, OnGlobalProcessed);
        }
        if (OnProcessed != null && AutoSubscribe)
        {
            Add(OnObjectProcessed, OnProcessed);
        }

        if (OnGlobalProcessed != null && AutoSubscribe)
        {
            Add(OnObjectProcessed, OnGlobalProcessed);
        }
        if (OnProcessed != null && GlobalAutoSubscribe)
        {
            Add(OnGlobalObjectProcessed, OnProcessed);
        }

        if (OnGlobalInspected != null && GlobalAutoSubscribe)
        {
            Add(OnGlobalObjectInspected, obj => OnGlobalInspected.Invoke(obj));
        }
        if (OnInspected != null && GlobalAutoSubscribe)
        {
            Add(OnObjectInspected, obj => OnInspected.Invoke(obj));
        }

        if (OnGlobalInspected != null && AutoSubscribe)
        {
            Add(OnObjectInspected, obj => OnGlobalInspected.Invoke(obj));
        }
        if (OnInspected != null && GlobalAutoSubscribe)
        {
            Add(OnGlobalObjectInspected, obj => OnInspected.Invoke(obj));
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        if (OnGlobalDetecting != null)
        {
            Remove(OnGlobalObjectDetecting, OnGlobalDetecting);
        }
        if (OnDetecting != null)
        {
            Remove(OnObjectDetecting, OnDetecting);
        }

        if (GlobalAutoSubscribe && OnDetecting.Count > 0)
        {
            foreach (var func in OnDetecting)
            {
                if (!OnGlobalObjectDetecting.Contains(func))
                    Remove(OnGlobalObjectDetecting, func);
            }
        }
        if (AutoSubscribe && OnGlobalDetecting.Count > 0)
        {
            foreach (var func in OnGlobalDetecting)
            {
                if (!OnObjectDetecting.Contains(func))
                    Remove(OnObjectDetecting, func);
            }
        }

        if (OnGlobalDetected != null)
        {
            Remove(OnGlobalObjectDetected, OnGlobalDetected);
        }
        if (OnDetected != null)
        {
            Remove(OnObjectDetected, OnDetected);
        }

        if (OnGlobalDetected != null)
        {
            Remove(OnObjectDetected, OnGlobalDetected);
        }
        if (OnDetected != null)
        {
            Remove(OnGlobalObjectDetected, OnDetected);
        }

        if (OnGlobalProcessed != null)
        {
            Remove(OnGlobalObjectProcessed, OnGlobalProcessed);
        }
        if (OnProcessed != null)
        {
            Remove(OnObjectProcessed, OnProcessed);
        }

        if (OnGlobalProcessed != null)
        {
            Remove(OnObjectProcessed, OnGlobalProcessed);
        }
        if (OnProcessed != null)
        {
            Remove(OnGlobalObjectProcessed, OnProcessed);
        }

        if (OnGlobalInspected != null)
        {
            Remove(OnGlobalObjectInspected, obj => OnGlobalInspected.Invoke(obj));
        }
        if (OnInspected != null)
        {
            Remove(OnObjectInspected, obj => OnInspected.Invoke(obj));
        }

        if (OnGlobalInspected != null)
        {
            Remove(OnObjectInspected, obj => OnGlobalInspected.Invoke(obj));
        }
        if (OnInspected != null)
        {
            Remove(OnGlobalObjectInspected, obj => OnInspected.Invoke(obj));
        }
    }

    public virtual void OnEnter2D(GameObject other)
    {
        GameObject obj;
        foreach (var globalObjectDetecting in OnGlobalObjectDetecting)
            obj = globalObjectDetecting(other);
        if (OnGlobalObjectDetected != null)
        {
            foreach (var handler in OnGlobalObjectDetected.GetInvocationList())
            {
                var func = handler as Func<GameObject, GameObject>;
                if (func != null)
                    obj = func(other);
            }
        }
        obj = OnGlobalObjectDetected?.Invoke(other);
        OnGlobalObjectProcessed?.Invoke(obj);
        OnGlobalObjectInspected?.Invoke(obj);
        if (OnObjectDetected != null)
        {
            foreach (var handler in OnObjectDetected.GetInvocationList())
            {
                var func = handler as Func<GameObject, GameObject>;
                if (func != null)
                    obj = func(other);
            }
        }
        obj = OnObjectDetected?.Invoke(other);
        OnObjectProcessed?.Invoke(obj);
        OnObjectInspected?.Invoke(obj);
    }
    public virtual void CollisionEnter2D(Collision2D collision)
    {
        OnEnter2D(collision.gameObject);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter2D(collision);
    }

    public virtual void CollisionStay2D(Collision2D collision)
    {
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Sigue colisionando con: " + collision.gameObject.name);
        CollisionStay2D(collision);
    }

    public virtual void CollisionExit2D(Collision2D collision)
    {
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        CollisionExit2D(collision);
    }

    public virtual void TriggerEnter2D(Collider2D collider)
    {
        OnEnter2D(collider.gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerEnter2D(collider);
    }

    public virtual void TriggerStay2D(Collider2D collider)
    {
    }
    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Está dentro del trigger con: " + collider.name);
        TriggerStay2D(collider);
    }

    public virtual void TriggerExit2D(Collider2D collider)
    {
    }
    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Salió del trigger con: " + collider.name);
        TriggerExit2D(collider);
    }

}
