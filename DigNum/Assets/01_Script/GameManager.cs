using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapSO mapSO;

    private void Start()
    {
        ResetMap();
    }

    private void ResetMap()
    {
        for (int i = 0; i < mapSO.map.GetLength(0); i++)
        {
            for (int j = 0; i <= mapSO.map.GetLength(1); j++)
            {
                mapSO.map[i, j] = 0;
            }
        }

        for (int i = 0; i < mapSO.itemMap.GetLength(0); i++)
        {
            for (int j = 0; i <= mapSO.itemMap.GetLength(1); j++)
            {
                mapSO.itemMap[i, j] = 0;
            }
        }
    }
    private void MoveDown()
    {
        for(int i = 0; i < mapSO.map.GetLength(1); i++)
        {
            //mapSO.map[0,i] = ;
        }
    }
}
