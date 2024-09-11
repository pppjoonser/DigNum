using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LandVeiwer : MonoBehaviour
{
    [SerializeField] MapSO _mapSO;
    [SerializeField] BlockSpriteByLevelSO _blockSpriteByLevelSO;
    [SerializeField] private GameObject _blockPrefab, _blockParent;
    private SpriteRenderer[,] spriteMap = new SpriteRenderer[20,12];
    private TextMeshPro[,] _textMeshPro = new TextMeshPro[20,12];

    public void Start()
    {
        GameManager.Instance.OnPlayerMove += SetVisual;
        CreateVisual();
    }
    private void CreateVisual()
    {
        for (int i = 0; i < _mapSO.map.GetLength(0); i++)
        {
            for(int j = 0; j< _mapSO.map.GetLength(1); j++)
            {
                GameObject Tile = Instantiate(_blockPrefab, transform.position, Quaternion.identity ,_blockParent.transform);
                Tile.transform.localPosition = new Vector3(j, i);
                spriteMap[i,j] = Tile.GetComponent<SpriteRenderer>();
                _textMeshPro[i,j] = Tile.GetComponentInChildren <TextMeshPro>();
            }
        }
        SetVisual();
    }
    private void SetVisual()
    {
        if (spriteMap == null || _textMeshPro == null) return;
        for (int i = 0; i < _mapSO.map.GetLength(0); i++)
        {
            for (int j = 0; j < _mapSO.map.GetLength(1); j++)
            {
                //foreach로 검사하면서 값 확인하고 쭉쭉 올리기
                foreach(Block block in _blockSpriteByLevelSO.blocks)
                {
                    if(_mapSO.map[i, j] >= block.blockLevel)
                    {
                        spriteMap[i,j].sprite = block.blockSprite;
                        _textMeshPro[i, j].text = _mapSO.map[i, j].ToString();
                    }
                }
            }
        }
    }
}