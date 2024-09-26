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
    public GameObject[,] _gameMap = new GameObject[12,20];//���ӿ�����Ʈ ��
    private SpriteRenderer[,] spriteMap = new SpriteRenderer[12,20];//�ʿ� �ִ°� ��������Ʈ
    private TextMeshPro[,] _textMeshPro = new TextMeshPro[12,20];//�ʿ� �ִ°� �ؽ�Ʈ

    public void Start()
    {
        GameManager.Instance.OnPlayerMove += SetVisualMap;
        CreateVisual();
    }
    private void CreateVisual()//���ܸ����� ����
    {
        for (int i = 0; i < _mapSO.map.GetLength(1); i++)
        {
            for(int j = 0; j< _mapSO.map.GetLength(0); j++)
            {
                GameObject Tile = Instantiate(_blockPrefab, transform.position, Quaternion.identity ,_blockParent.transform);
                _gameMap[j,i] = Tile;
                Tile.transform.localPosition = new Vector3(j, -i);//�̰� i�� -1�� ���ϸ� �ݴ�� ����
                spriteMap[j,i] = Tile.GetComponent<SpriteRenderer>();
                _textMeshPro[j, i] = Tile.GetComponentInChildren <TextMeshPro>();
            }
        }
        SetVisualMap();
    }
    private void SetVisualMap()//�̰� ���� ���ܸ���?->�����ֱ�
    {
        if (spriteMap == null || _textMeshPro == null) return;
        for (int i = 0; i < _mapSO.map.GetLength(1); i++)
        {
            for (int j = 0; j < _mapSO.map.GetLength(0); j++)
            {
                //foreach�� �˻��ϸ鼭 �� Ȯ���ϰ� ���� �ø���
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