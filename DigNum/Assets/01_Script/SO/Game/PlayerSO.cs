using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/player")]
public class PlayerSO : ScriptableObject
{
    public int fuel;
    public int maxFuel;
    public int digPower;
}
