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

    private void Awake()//�ν��Ͻ�
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


    private void ResetMap()//���� �����Ҷ� �� ���� ��°�
    {
        // �ʰ� ������ �� �ʱ�ȭ
        for (int i = 0; i < mapSO.map.GetLength(1); i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(0); j++)
            {
                mapSO.map[j, i] = 0;
                mapSO.itemMap[j, i] = 0;
            }
        }

        // 20�� ���� �Ʒ��� �̵�
        StartCoroutine(TestCoroutine());
    }

    public void PlayerMoveDown()//��������
    {
        for (int i = 0; i < mapSO.map.GetLength(1) - 1; i++)
        {
            for (int j = 0; j < mapSO.map.GetLength(0); j++)
            {
                mapSO.map[j, i] = mapSO.map[j,i + 1 ];
                mapSO.itemMap[j, i] = mapSO.itemMap[j, i + 1];
            }
        }

        // ������ �࿡ ���ο� �� ����
        for (int i = 0; i < mapSO.map.GetLength(0); i++)
        {
            int landHardness = Random.Range(1, levelSO.hardness + 1);
            mapSO.map[i, mapSO.map.GetLength(1) - 1] = landHardness;

            float rand = Random.Range(0, 100);
            if (rand > levelSO.itemSpawn)
            {
                mapSO.itemMap[i, mapSO.itemMap.GetLength(1) - 1] = Random.Range(0, 9);
            }
        }
        OnPlayerMove?.Invoke();
    }

    private IEnumerator TestCoroutine()//���
    {
        for (int i = 0; i < 14; i++)
        {
            PlayerMoveDown();
            yield return new WaitForSeconds(0.5f);

        }
    }
}