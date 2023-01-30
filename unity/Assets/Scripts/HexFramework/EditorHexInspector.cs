#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorHex))]
[CanEditMultipleObjects]
public class EditorHexInspector : Editor
{
	private const int buttonSize = 20;
	EditorHex _hex;

	private void OnEnable()
	{
		_hex = target as EditorHex;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (_hex.Designer == null)
			return;
		_hex.Designer.HexGrid.GetNeighbours(_hex.HexPos, out var hexes);
		
		GUILayout.BeginHorizontal(GUILayout.Height(buttonSize));
		GUILayout.Space(buttonSize);
		ShowButton(hexes[(int)HexGrid.Direction.North]);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal(GUILayout.Height(buttonSize));
		ShowButton(hexes[(int)HexGrid.Direction.North_West]);
		GUILayout.Space(buttonSize);
		ShowButton(hexes[(int)HexGrid.Direction.North_East]);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal(GUILayout.Height(buttonSize));
		ShowButton(hexes[(int)HexGrid.Direction.South_West]);
		GUILayout.Space(buttonSize);
		ShowButton(hexes[(int)HexGrid.Direction.South_East]);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal(GUILayout.Height(buttonSize));
		GUILayout.Space(buttonSize);
		ShowButton(hexes[(int)HexGrid.Direction.South]);
		GUILayout.EndHorizontal();
	}

	private void ShowButton(Hex hex)
	{
		if (hex == null)
		{
			GUILayout.Space(buttonSize);
			return;
		}

		if (GUILayout.Button("", GUILayout.Width(buttonSize)))
		{
			var hex_edit = _hex.Designer.GetHexAt(hex.Pos);
			Selection.SetActiveObjectWithContext(hex_edit.gameObject, null);
		}
	}

}
#endif