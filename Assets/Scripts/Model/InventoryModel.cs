using UnityEngine;

[CreateAssetMenu(fileName = "InventoryModel", menuName = "Models/InventoryItem")]
public class InventoryModel : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    [TextArea(3, 10)]
    public string description = "A generic item";

    public virtual void Use()
    {
        UnityEngine.Debug.Log("Using " + name);
    }
}
