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
    private GameObject[,] _gameMap = new GameObject[20,12];//���ӿ�����Ʈ ��
    private SpriteRenderer[,] spriteMap = new SpriteRenderer[20,12];//�ʿ� �ִ°� ��������Ʈ
    private TextMeshPro[,] _textMeshPro = new TextMeshPro[20,12];//�ʿ� �ִ°� �ؽ�Ʈ

    public void Start()
    {
        GameManager.Instance.OnPlayerMove += SetVisual;
        CreateVisual();
    }
    private void CreateVisual()//���ܸ����� ����
    {
        for (int i = 0; i < _mapSO.map.GetLength(0); i++)
        {
            for(int j = 0; j< _mapSO.map.GetLength(1); j++)
            {
                GameObject Tile = Instantiate(_blockPrefab, transform.position, Quaternion.identity ,_blockParent.transform);
                _gameMap[i,j] = Tile;
                Tile.transform.localPosition = new Vector3(j, -i);//�̰� i�� -1�� ���ϸ� �ݴ�� ����
                spriteMap[i,j] = Tile.GetComponent<SpriteRenderer>();
                _textMeshPro[i,j] = Tile.GetComponentInChildren <TextMeshPro>();
            }
        }
        SetVisual();
    }
    private void SetVisual()//�̰� ���� ���ܸ���?->�����ֱ�
    {
        if (spriteMap == null || _textMeshPro == null) return;
        for (int i = 0; i < _mapSO.map.GetLength(0); i++)
        {
            for (int j = 0; j < _mapSO.map.GetLength(1); j++)
            {
                //foreach�� �˻��ϸ鼭 �� Ȯ���ϰ� ���� �ø���
                foreach(Block block in _blockSpriteByLevelSO.blocks)
                {
                    if (_mapSO.map[i, j] == 0) 
                    {
                        spriteMap[i, j].sprite = null;
                        spriteMap[i, j].color = Color.clear;
                        _textMeshPro[i,j].text = null;
                    }
                    else
                    {
                        spriteMap[i, j].color = Color.white;
                    }
                    
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