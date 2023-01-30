using UnityEngine;

public static class Vector2IntExtension
{
    // Unity's .GetHashCode causes too many collisions
    public static long Hash(this Vector2Int v2)
    {
        //var hash = 23;
        //hash = hash * 31 + v2.x;
        //hash = hash * 31 + v2.y;
        //return hash
        //// no more playing nice
        return (long)v2.x << 32 | (long)(uint)v2.y;
    }
}