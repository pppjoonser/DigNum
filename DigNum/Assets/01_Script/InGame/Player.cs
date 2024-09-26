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
        // 새로운 위치 계산
        Vector2Int newPosition = currentPlayerPosition + new Vector2Int((int)input.x, -(int)input.y);

        // 맵 범위를 벗어나지 않는지 체크
        if (newPosition.x < 0 || newPosition.x >= _mapSO.map.GetLength(0) || newPosition.y < 0 || newPosition.y >= _mapSO.map.GetLength(1))
        {
            return; // 맵 범위를 벗어나면 이동하지 않음
        }

        // 실제 플레이어 오브젝트의 위치 이동
        if (CheckTargetEmpty(newPosition))
        {
            currentPlayerPosition = new Vector2Int( newPosition.x, currentPlayerPosition.y);
            transform.position = new Vector3(currentPlayerPosition.x - 5.5f, -currentPlayerPosition.y + 5, 0);  // 좌표를 화면 상 위치로 변환

            // 만약 아래로 이동했을 때 빈 공간이라면 맵을 아래로 이동
            if (Mathf.Abs(input.y) >= 1)
            {
                GameManager.Instance.PlayerMoveDown();
            }
        }
    }

}
