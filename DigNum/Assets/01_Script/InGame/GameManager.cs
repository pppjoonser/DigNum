using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapSO mapSO;
    [SerializeField]
    private LevelSO levelSO;
    [SerializeField]
    private ItemListSO itemList;

    public event Action OnPlayerMove;

    public static GameManager Instance { get; private set; }

    private void Awake()//인스턴스
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ResetMap();
    }


    private void ResetMap()//게임 시작할때 맵 새로 까는거
    {
        // 맵과 아이템 맵 초기화
        for (int i = 0; i < mapSO.map.GetLength(1); i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(0); j++)
            {
                mapSO.map[j, i] = 0;
                mapSO.itemMap[j, i] = null;
            }
        }

        // 20번 맵을 아래로 이동
        StartCoroutine(TestCoroutine());
    }

    public void PlayerMoveDown()//내려갈때
    {
        for (int i = 0; i < mapSO.map.GetLength(1) - 1; i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(0); j++)
            {
                mapSO.map[j, i] = mapSO.map[j,i + 1 ];
                mapSO.itemMap[j, i] = mapSO.itemMap[j, i + 1];
            }
        }

        // 마지막 행에 새로운 값 생성
        for (int i = 0; i < mapSO.map.GetLength(0); i++)
        {
            int landHardness = Random.Range(1, levelSO.hardness + 1);
            mapSO.map[i, mapSO.map.GetLength(1) - 1] = landHardness;

            float rand = Random.Range(0, 100);
            if (rand > levelSO.itemSpawn)
            {
                mapSO.itemMap[i, mapSO.itemMap.GetLength(1) - 1] = itemList.items[Random.Range(0, itemList.items.Count)];
            }
        }
        TryMove();
    }
    
    public void TryMove()
    {
        OnPlayerMove?.Invoke();

    }

    private IEnumerator TestCoroutine()//잡것
    {
        for (int i = 0; i < 14; i++)
        {
            PlayerMoveDown();
            yield return new WaitForSeconds(0.5f);

        }
    }

    public void CheckItem(Vector2Int diggingPosition)
    {
        if (mapSO.itemMap[diggingPosition.x, diggingPosition.y] != null)
        {
            if(mapSO.map[diggingPosition.x, diggingPosition.y]<=0)
            ItemManager.Instance.AddItem(mapSO.itemMap[diggingPosition.x, diggingPosition.y]);
        }
    }
}