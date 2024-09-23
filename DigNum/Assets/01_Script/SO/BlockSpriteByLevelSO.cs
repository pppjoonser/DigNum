using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/block")]
public class BlockSpriteByLevelSO : ScriptableObject
{
    public Block[] blocks;
}
[Serializable]
public struct Block
{
    public int blockLevel;//블록 단단함
    public Sprite blockSprite;//블록 생겨먹은거
}
