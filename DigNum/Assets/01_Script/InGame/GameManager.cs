using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapSO mapSO;
    [SerializeField]
    private LevelSO levelSO;

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
        for (int i = 0; i < mapSO.map.GetLength(0); i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(1); j++)
            {
                mapSO.map[i, j] = 0;
                mapSO.itemMap[i, j] = 0;
            }
        }

        // 20번 맵을 아래로 이동
        StartCoroutine(TestCoroutine());
    }

    public void PlayerMoveDown()//내려갈때
    {
        for (int i = 0; i < mapSO.map.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(1); j++)
            {
                mapSO.map[i, j] = mapSO.map[i + 1, j];
                mapSO.itemMap[i, j] = mapSO.itemMap[i + 1, j];
            }
        }

        // 마지막 행에 새로운 값 생성
        for (int i = 0; i < mapSO.map.GetLength(1); i++)
        {
            int landHardness = Random.Range(1, levelSO.hardness + 1);
            mapSO.map[mapSO.map.GetLength(0) - 1, i] = landHardness;

            float rand = Random.Range(0, 100);
            if (rand > levelSO.itemSpawn)
            {
                mapSO.itemMap[mapSO.itemMap.GetLength(0) - 1, i] = Random.Range(0, 9);
            }
        }
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
}