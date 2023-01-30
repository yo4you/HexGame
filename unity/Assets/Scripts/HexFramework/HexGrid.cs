using System.Collections.Generic;
using UnityEngine;

public class HexGrid
{
    private Dictionary<long, Hex> _grid = new Dictionary<long, Hex>();
    public Vector2Int Center { get; private set; } = new Vector2Int();
    public enum Direction : int
    {
        North,
        North_East,
        South_East,
        South,
        South_West,
        North_West,
    }
    public static readonly Vector2Int[] Directions = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
    };
    // TODO: this is perhaps not very performant, hardcode this if this is used frequently 
    public static Vector2Int North_East => Directions[(int)Direction.North_East];
    public static Vector2Int North => Directions[(int)Direction.North];
    public static Vector2Int South_East => Directions[(int)Direction.South_East];
    public static Vector2Int South_West => Directions[(int)Direction.South_West];
    public static Vector2Int South => Directions[(int)Direction.South];
    public static Vector2Int North_West => Directions[(int)Direction.North_West];

    const float Half_Sqrt_3 = 0.866025403785f;
    const float Sqrt_3 = 1.73205080757f;
    public static Vector2 GetPos(Vector2Int hex_axis)
    {
        return new Vector2(
            1.5f * hex_axis.x,
            Half_Sqrt_3 * hex_axis.x + Sqrt_3 * hex_axis.y);
    }
    public void ShiftGrid(Vector2Int axis)
    {
        Center += axis;
        var new_grid = new Dictionary<long, Hex>();

        foreach (var item in _grid)
        {
            var hex = item.Value;
            new_grid.Add((hex.Pos + axis).Hash(), new Hex(hex.Pos)
            {
                Type = hex.Type
            });
        }
        _grid = new_grid;
    }
    public bool GetHex(Vector2Int hex_axis, out Hex hex)
    {
        var hash = hex_axis.Hash();
        return _grid.TryGetValue(hash, out hex);
    }
    // order : North_East, East, South_East, South_West, West, North_West  
    public void GetNeighbours(Vector2Int hex, out Hex[] neighbours)
    {
        neighbours = new Hex[]{
            GetNeighbour(hex,  North),
            GetNeighbour(hex,  North_East),
            GetNeighbour(hex,  South_East),
            GetNeighbour(hex,  South),
            GetNeighbour(hex,  South_West),
            GetNeighbour(hex,  North_West),
        };
    }

    public Hex GetNeighbour(Vector2Int pos, Vector2Int neighbour)
    {
        GetHex(pos + neighbour, out var n_hex);
        return n_hex;
    }

    public void SetHex(Vector2Int axis, Hex hex)
    {
        var hash = axis.Hash();
        if (_grid.ContainsKey(hash))
        {
            _grid[hash] = hex;
        }
        else
        {
            _grid.Add(hash, hex);
        }
    }
    public void RemoveHex(Vector2Int axis)
    {
        _grid.Remove(axis.Hash());
    }
}

public enum HexTypes
{
    Uknown,
    Forest,
    Castle,
    Bee,
}

public class Hex
{
    public HexTypes Type { get; set; }

    // This is mostly used by the hexgrid to remap a grid chunk
    public Vector2Int Pos { get; private set; }
    public Hex(Vector2Int pos)
    {
        Pos = pos;
    }

    public static Vector2Int GetAxial(Vector3Int cube)
    {
        return (Vector2Int)cube;
    }
    public static Vector3Int GetCube(Vector2Int Axis)
    {
        var q = Axis.x;
        var r = Axis.y;
        var s = -q - r;
        return new Vector3Int(q, r, s);
    }
}
