using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public int[,] _playerPosition = new int[12, 20];
    public int[,] _targetposition = new int[12, 20];

    private Vector2Int currentPlayerPosition;

    [SerializeField]
    private MapSO _mapSO;
    [SerializeField]
    private PlayerSO _playerData;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                _playerPosition[j, i] = 0;
                _targetposition[j, i] = 0;
            }
        }
        currentPlayerPosition = new Vector2Int(5, 5);


        if (InputReader.Instance != null)
        {
            InputReader.Instance.OnMove += PlayerMove;
        }
        else
        {
            Debug.Log("InputReader.Instance is null!");
        }

        _playerData.fuel = _playerData.maxFuel;
    }

    private void OnDisable()
    {
        InputReader.Instance.OnMove -= PlayerMove;
    }

    private bool CheckTargetEmpty(Vector2Int target)
    {
        DigTheBlock(target);
        if (_mapSO.map[target.x, target.y] <= 0)
        {
            return true;
        }
        return false;

    }

    private void DigTheBlock(Vector2Int blockpoint)
    {
        if (_mapSO.map[blockpoint.x, blockpoint.y] <= 0)
        {
            return;
        }
        
        if(_playerData.fuel > 0)
        {
            _mapSO.map[blockpoint.x,blockpoint.y]-= _playerData.digPower;
            _playerData.fuel--;
            GameManager.Instance.TryMove();
        }
    }

    private void PlayerMove(Vector2 input)
    {
        // ���ο� ��ġ ���
        Vector2Int newPosition = currentPlayerPosition + new Vector2Int((int)input.x, -(int)input.y);

        // �� ������ ����� �ʴ��� üũ
        if (newPosition.x < 0 || newPosition.x >= _mapSO.map.GetLength(0) || newPosition.y < 0 || newPosition.y >= _mapSO.map.GetLength(1))
        {
            return; // �� ������ ����� �̵����� ����
        }

        // ���� �÷��̾� ������Ʈ�� ��ġ �̵�
        if (CheckTargetEmpty(newPosition))
        {
            currentPlayerPosition = new Vector2Int( newPosition.x, currentPlayerPosition.y);
            transform.position = new Vector3(currentPlayerPosition.x - 5.5f, -currentPlayerPosition.y + 5, 0);  // ��ǥ�� ȭ�� �� ��ġ�� ��ȯ

            // ���� �Ʒ��� �̵����� �� �� �����̶�� ���� �Ʒ��� �̵�
            if (Mathf.Abs(input.y) >= 1)
            {
                GameManager.Instance.PlayerMoveDown();
            }
        }
    }

}
