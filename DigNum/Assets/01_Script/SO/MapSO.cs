using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/_mapSO")]
public class MapSO : ScriptableObject
{
    public int[,] map = new int[12,20];
    public int[,] itemMap = new int[12,20];
}
