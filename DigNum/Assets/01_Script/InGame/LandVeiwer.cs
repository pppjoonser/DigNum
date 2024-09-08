using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LandVeiwer : MonoBehaviour
{
    [SerializeField] MapSO _mapSO;
    [SerializeField] private GameObject _blockPrefab, _blockParent;
    

    public void Awake()
    {
        CreateVisual();
    }
    private void CreateVisual()
    {
        for (int i = 0; i < _mapSO.map.GetLength(0); i++)
        {
            for(int j = 0; j< _mapSO.map.GetLength(1); j++)
            {
                Instantiate(_blockPrefab,new Vector3(j,i), Quaternion.identity ,_blockParent.transform);
            }
        }
    }
    private void SetVisual()
    {
        for (int i = 0; i < _mapSO.map.GetLength(0); i++)
        {
            for (int j = 0; j < _mapSO.map.GetLength(1); j++)
            {
                //foreach로 검사하면서 값 확인하고 쭉쭉 올리기
            }
        }
    }
}