using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private void OnEnable()
    {
        for (int i = 0; i < ItemManager.Instance.inventoryItems.Count; i++)
        {
            Instantiate(ItemManager.Instance.inventoryItems[i].itemPrefab);
        }
    }
}
