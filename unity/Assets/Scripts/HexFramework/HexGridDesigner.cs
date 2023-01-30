using System;
using System.Collections.Generic;
using UnityEngine;

public class HexGridDesigner : MonoBehaviour
{
	public HexGrid HexGrid { get; private set; } = new HexGrid();
    [SerializeField] private int _designRadius = 5;
	[SerializeField] private EditorHex _hexDesign;
    private float _hexSize = 1f;
    private List<EditorHex> _hexes = new List<EditorHex>();

	public EditorHex GetHexAt(Vector2Int pos)
    {
		return _hexes.Find(i => i.HexPos == pos);
    }

	private void OnValidate()
	{
		var offsets = HexGrid.Directions;
		var to_destroy = new EditorHex[_hexes.Count];
		_hexes.CopyTo(to_destroy);
		// Not allowed to destroy in OnValidate
		UnityEditor.EditorApplication.delayCall += () =>
		{
			foreach (var item in to_destroy)
			{
				if(item)DestroyImmediate(item.gameObject);
			}
		};
		_hexes = new List<EditorHex>();
		HexGrid = new HexGrid();

		GenerateHexAt(new Vector2Int(0, 0));
		for (int radius = 1; radius < _designRadius + 1; radius++)
		{
			for (int corner = 0; corner < 6; corner++)
			{
				var start = offsets[corner] * radius;
				var end = offsets[(corner + 1) % 6] * radius;
				// since we're on a grid we can use Sign to normalize this 
				var direction = new Vector2Int(
					Math.Sign(end.x - start.x),
					Math.Sign(end.y - start.y));
				
				// hexagonal ring of size n has n hexagons between the corners
				for (int segment = 0; segment < radius ; segment++)
				{
					var pos = start + direction * segment;
					GenerateHexAt(pos);
				}
			}
		}
		Redraw();
    }

    internal void Redraw()
    {
        foreach (var hex in _hexes)
        {
			if(hex)
				hex.Redraw();
        }
    }
	static int a = 0;
    private void GenerateHexAt(Vector2Int pos)
    {
        var hex = Instantiate(_hexDesign, transform);
		hex.Designer = this;
        _hexes.Add(hex);
        hex.HexPos = hex.PreviousPos = pos;
		hex.name = (++a).ToString();
		HexGrid.SetHex(pos, new Hex(pos));
    }

	public Vector2 GetWorldPos(Vector2Int pos)
    {
		return HexGrid.GetPos(pos) * _hexSize;
	}
}
