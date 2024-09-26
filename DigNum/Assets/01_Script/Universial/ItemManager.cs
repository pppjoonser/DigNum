using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    List<ItemSO> inventoryItems = new List<ItemSO>();
    public static ItemManager Instance {  get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(ItemSO item)
    {
        inventoryItems.Add(item);
    }
    public void RemoveItem(ItemSO item)
    {
        inventoryItems.Remove(item);
    }
}
