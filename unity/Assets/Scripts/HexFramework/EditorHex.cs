using UnityEngine;

[ExecuteInEditMode]
public class EditorHex : MonoBehaviour
{

    [SerializeField] HexTypes HexType = HexTypes.Uknown;
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] public Vector2Int HexPos;
    [SerializeField] TileData _tileData;
    public Vector2Int PreviousPos { get; set; }
    public HexGridDesigner Designer { get; set; }

    public void OnValidate()
    {
        if (Designer == null) 
            return;
        if (!Designer.HexGrid.GetHex(HexPos, out var _))
        {
            Designer.HexGrid.RemoveHex(PreviousPos);
        }
        else
        {
            HexPos = PreviousPos;
        }
        var new_hex = new Hex(HexPos)
        {
            Type = HexType,
        };
        Designer.HexGrid.SetHex(HexPos, new_hex);

        PreviousPos = HexPos;

        Designer.Redraw();
    }
    internal void Redraw()
    {
        if (Designer.HexGrid.GetHex(HexPos, out var hex))
        {
            _sprite.sprite = _tileData.TileSprites[hex.Type];
            transform.position = Designer.GetWorldPos(HexPos);
        }
    }
}

