using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/map")]
public class MapSO : ScriptableObject
{
    public int[,] map = new int[20,12];
    public int[,] itemMap = new int[20,12];
}
