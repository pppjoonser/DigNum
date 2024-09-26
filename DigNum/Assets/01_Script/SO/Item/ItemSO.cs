using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/item")]
public class ItemSO : ScriptableObject
{
    public GameObject itemPrefab;
    public int numberInQuestion;
    public string nameOfItem;
}
