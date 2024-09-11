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

    private void Awake()
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

    private void ResetMap()
    {
        // �ʰ� ������ �� �ʱ�ȭ
        for (int i = 0; i < mapSO.map.GetLength(0); i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(1); j++)
            {
                mapSO.map[i, j] = 0;
                mapSO.itemMap[i, j] = 0;
            }
        }

        // 20�� ���� �Ʒ��� �̵�
        StartCoroutine(TestCoroutine());
    }

    private void PlayerMoveDown()
    {
        for (int i = 0; i < mapSO.map.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(1); j++)
            {
                mapSO.map[i, j] = mapSO.map[i + 1, j];
                mapSO.itemMap[i, j] = mapSO.itemMap[i + 1, j];
            }
        }

        // ������ �࿡ ���ο� �� ����
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

    private IEnumerator TestCoroutine()
    {
        for (int i = 0; i < 20; i++)
        {
            PlayerMoveDown();
            yield return new WaitForSeconds(0.5f);

        }
    }
}