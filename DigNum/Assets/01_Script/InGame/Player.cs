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
        currentPlayerPosition = new Vector2Int(6, 6);
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
        Vector2Int newPosition = currentPlayerPosition + new Vector2Int(0, (int)input.x);

        // 맵 범위를 벗어나지 않는지 체크
        if (newPosition.y < 0 || newPosition.y >= 12)
        {
            return; // 맵 범위를 벗어나면 이동하지 않음
        }

        Debug.Log(newPosition);
        // 플레이어의 이전 위치를 0으로 설정
        _playerPosition[currentPlayerPosition.x, currentPlayerPosition.y] = 0;

        // 새로운 위치에 플레이어 이동
        currentPlayerPosition = newPosition;
        _playerPosition[currentPlayerPosition.x, currentPlayerPosition.y] = 1;

        if (Mathf.Abs(input.y) >= 1 && CheckTargetEmpty(currentPlayerPosition + new Vector2Int(1,0)))
        {
            GameManager.Instance.PlayerMoveDown();
        }
    }
}
