using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/item list")]
public class ItemListSO : ScriptableObject
{
    public List<ItemSO> items;
}
