using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TileData : ScriptableObject
{
    [SerializeField] List<TileSprite> _tileSprites;
    public Dictionary<HexTypes, Sprite> TileSprites { get; private set; }

    public void OnValidate()
    {
        TileSprites = new Dictionary<HexTypes, Sprite>();
        foreach (var tilesprite in _tileSprites)
        {
            TileSprites.Add(tilesprite.HexType, tilesprite.Sprite);
        }
    }
    
    // This is just a stand-in for the fact the fact that unity won't serialize Dictionaries
    [Serializable]
    public class TileSprite
    {
        public HexTypes HexType;
        public Sprite Sprite;
    }
}