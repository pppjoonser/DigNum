using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapSO mapSO;
    [SerializeField]
    private LevelSO levelSO;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ResetMap();
    }

    private void ResetMap()
    {
        for (int i = 0; i < mapSO.map.GetLength(0); i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(1); j++)
            {
                mapSO.map[i, j] = 0;
            }
        }

        for (int i = 0; i < mapSO.itemMap.GetLength(0); i++)
        {
            for (int j = 0; j < mapSO.itemMap.GetLength(1); j++)
            {
                mapSO.itemMap[i, j] = 0;
            }
        }
    }
    private void MoveDown()
    {
        for(int i = 0; i < mapSO.itemMap.GetLength(1); i++)
        {
            float rand = Random.Range(0, 100);
            if(rand > levelSO.itemSpawn) mapSO.itemMap[mapSO.itemMap.GetLength(0)-1, i] = Random.Range(0,9);            
        }

        for(int i = 0; i < mapSO.map.GetLength(1); i++)
        {
            int landHardNess = Random.Range(1, levelSO.hardness+1);
            mapSO.map[mapSO.map.GetLength(0) - 1, i] = landHardNess;
        }
    }
}
