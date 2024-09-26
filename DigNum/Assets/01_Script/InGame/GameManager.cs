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
                mapSO.itemMap[j, i] = null;
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
                mapSO.itemMap[i, mapSO.itemMap.GetLength(1) - 1] = itemList.items[Random.Range(0, itemList.items.Count)];
            }
        }
        TryMove();
    }
    
    public void TryMove()
    {
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

    public void CheckItem(Vector2Int diggingPosition)
    {
        if (mapSO.itemMap[diggingPosition.x, diggingPosition.y] != null)
        {
            if(mapSO.map[diggingPosition.x, diggingPosition.y]<=0)
            ItemManager.Instance.AddItem(mapSO.itemMap[diggingPosition.x, diggingPosition.y]);
        }
    }
}