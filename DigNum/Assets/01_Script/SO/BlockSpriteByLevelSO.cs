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
    public int blockLevel;
    public Sprite blockSprite;
}
