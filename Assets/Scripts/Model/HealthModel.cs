using UnityEngine;

/// <summary>
/// Represebts the current vital statistics of some game entity.
/// </summary>
[CreateAssetMenu(fileName = "HealthModel", menuName = "Models/Health", order = 0)]
public class HealthModel : ScriptableObject
{
    [SerializeField] MonoController entity;
    /// <summary>
    /// Increment the HP of the entity.
    /// </summary>
    public void Increment()
    {
        // If the current HP is already at max, do nothing.
        entity.currentHP = Mathf.Clamp(entity.currentHP + 1, 0, entity.maxHP);
    }

    /// <summary>
    /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
    /// current HP reaches 0.
    /// </summary>
    public virtual void Decrement()
    {
        // If the current HP is already at 0, do nothing.
        entity.currentHP = Mathf.Clamp(entity.currentHP - 1, 0, entity.maxHP);
    }

    /// <summary>
    /// Decrement the HP of the entitiy until HP reaches 0.
    /// </summary>
    public void Die()
    {
        // If the current HP is already at 0, do nothing.
        while (entity.currentHP > 0) Decrement();
    }

    /// <summary>
    /// 
    /// Called when the script is first loaded or when the game object is instantiated.
    /// 
    /// </summary>
    protected virtual void Awake()
    {
        // Initialize the current HP to the maximum HP.
        entity.currentHP = entity.maxHP;
    }
}