using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public int[,] _playerPosition = new int[20, 12];
    public int[,] _targetposition = new int[20, 12];

    private Vector2Int currentPlayerPosition;

    [SerializeField]
    private MapSO _mapSO;

    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                _playerPosition[i, j] = 0;
                _targetposition[i, j] = 0;
            }
        }
        currentPlayerPosition = new Vector2Int(0, 0);
    }

    private void OnEnable()
    {
         InputReader.Instance.OnMove += PlayerMove;
    }

    private bool CheckTargetEmpty(Vector2Int target)
    {
        print(_mapSO.map[target.x, target.y]);
        if(_mapSO.map[target.x, target.y] == 0)
        {
            return true;
        }
        return false;
    }

    private void PlayerMove(Vector2 input)
    {
        Vector2Int newPosition = currentPlayerPosition + new Vector2Int((int)input.x, 0);

        // �� ������ ����� �ʴ��� üũ
        if (newPosition.x < 0 || newPosition.x >= 12)
        {
            return; // �� ������ ����� �̵����� ����
        }

        Debug.Log(newPosition);
        // �÷��̾��� ���� ��ġ�� 0���� ����
        _playerPosition[currentPlayerPosition.x, currentPlayerPosition.y] = 0;

        // ���ο� ��ġ�� �÷��̾� �̵�
        currentPlayerPosition = newPosition;
        _playerPosition[currentPlayerPosition.x, currentPlayerPosition.y] = 1;

        if (Mathf.Abs(input.y) >= 1 && CheckTargetEmpty(currentPlayerPosition + new Vector2Int(0,1)))
        {
            GameManager.Instance.PlayerMoveDown();
        }
    }
}
