using UnityEngine;
using System.Collections.Generic;
using System;

public class KnapsackModel : MonoBehaviour
{
    public static KnapsackModel instance;
    public event Action<KnapsackModel> OnInventoryChanged;

    [Header("Inventory Settings")]
    [SerializeField] private int inventoryCapacity = 3;

    public List<InventoryModel> items = new List<InventoryModel>();

    void Awake()
    {
        if (instance == null)
        {
            Debug.LogWarning("More than one instance found");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public bool AddItem(InventoryModel item)
    {
        if (items.Count > inventoryCapacity)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        items.Add(item);
        OnInventoryChanged?.Invoke(instance);
        Debug.Log($"Added item: {item.name}");
        return true;
    }

    public bool RemoveItem(InventoryModel item)
    {
        bool removed = items.Remove(item);
        if (removed)
        {
            OnInventoryChanged?.Invoke(instance);
            Debug.Log($"Removed item: {item.name}");
        }
        else
        {
            Debug.Log($"Could not find item to remove: {item.name}");
        }
        return removed;
    }

    public bool UseItem(InventoryModel item)
    {
        if (items.Contains(item))
        {
            item.Use();

            RemoveItem(item);
        }
        else
        {
            Debug.Log($"Cannot use ${item.name}: not in inventory");
        }
        return false;
    }

    public List<InventoryModel> GetItems()
    {
        return new List<InventoryModel>(items);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Current Inventory:");
            foreach (var coin in items)
            {
                Debug.Log("- " + coin.name);
            }
        }
    }

}