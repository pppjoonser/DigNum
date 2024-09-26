using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LandVeiwer : MonoBehaviour
{
    [SerializeField] MapSO _mapSO;
    [SerializeField] BlockSpriteByLevelSO _blockSpriteByLevelSO;
    [SerializeField] private GameObject _blockPrefab, _blockParent;
    public GameObject[,] _gameMap = new GameObject[12,20];//게임오브젝트 맵
    private SpriteRenderer[,] spriteMap = new SpriteRenderer[12,20];//맵에 있는거 스프라이트
    private TextMeshPro[,] _textMeshPro = new TextMeshPro[12,20];//맵에 있는거 텍스트

    public void Start()
    {
        GameManager.Instance.OnPlayerMove += SetVisualMap;
        CreateVisual();
    }
    private void CreateVisual()//생겨먹은거 설정
    {
        for (int i = 0; i < _mapSO.map.GetLength(1); i++)
        {
            for(int j = 0; j< _mapSO.map.GetLength(0); j++)
            {
                GameObject Tile = Instantiate(_blockPrefab, transform.position, Quaternion.identity ,_blockParent.transform);
                _gameMap[j,i] = Tile;
                Tile.transform.localPosition = new Vector3(j, -i);//이거 i에 -1안 곱하면 반대로 나옴
                spriteMap[j,i] = Tile.GetComponent<SpriteRenderer>();
                _textMeshPro[j, i] = Tile.GetComponentInChildren <TextMeshPro>();
            }
        }
        SetVisualMap();
    }
    private void SetVisualMap()//이거 어케 생겨먹음?->보여주기
    {
        if (spriteMap == null || _textMeshPro == null) return;
        for (int i = 0; i < _mapSO.map.GetLength(1); i++)
        {
            for (int j = 0; j < _mapSO.map.GetLength(0); j++)
            {
                //foreach로 검사하면서 값 확인하고 쭉쭉 올리기
                foreach(Block block in _blockSpriteByLevelSO.blocks)
                {
                    if (_mapSO.map[j, i] <= 0) 
                    {
                        spriteMap[j, i].sprite = null;
                        spriteMap[j, i].color = Color.clear;
                        _textMeshPro[j, i].text = null;
                    }
                    else
                    {
                        spriteMap[j, i].color = Color.white;
                    }
                    
                    if(_mapSO.map[j, i] >= block.blockLevel)
                    {
                        spriteMap[j, i].sprite = block.blockSprite;
                        _textMeshPro[j, i].text = _mapSO.map[j, i].ToString();
                    }
                }
            }
        }
    }
}