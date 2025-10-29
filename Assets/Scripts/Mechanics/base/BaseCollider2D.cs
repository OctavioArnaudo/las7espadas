using UnityEngine;

[
    RequireComponent(
        typeof(Collider2D)
    )
]
public class BaseCollider2D : BaseCinemachine
{
    /*internal new*/
    [SerializeField] protected Collider2D c2D;

    /// <summary>
    /// 
    /// The Bounds property returns the bounds of the enemy's collider.
    /// 
    /// </summary>
    [SerializeField] protected Bounds bounds => c2D.bounds;

    protected override void Awake()
    {
        base.Awake();
        c2D = GetComponent<Collider2D>();
    }

    public virtual void MouseOver()
    {
    }
    protected virtual void OnMouseOver()
    {
        MouseOver();
    }

    public virtual void MouseEnter()
    {
    }
    protected virtual void OnMouseEnter()
    {
        MouseEnter();
    }

    public virtual void MouseDown()
    {
    }
    protected virtual void OnMouseDown()
    {
        MouseDown();
    }

    public virtual void MouseExit()
    {
    }
    protected virtual void OnMouseExit()
    {
        Debug.Log("Mouse salió del objeto");
        MouseExit();
    }

    public virtual void MouseUp()
    {
    }
    protected virtual void OnMouseUp()
    {
        Debug.Log("Soltó el click en el objeto");
        MouseUp();
    }

    public virtual void MouseDrag()
    {
    }
    protected virtual void OnMouseDrag()
    {
        Debug.Log("Arrastrando el objeto con el mouse");
        MouseDrag();
    }

}