using UnityEngine;

/**
This class contains general extensions that can be used in the project
*/
public static class GeneralExtensions
{
    public static Vector2 ToV2(this Vector3 v) => new Vector2(v.x, v.y);
    public static Vector3 ToV3(this Vector2 v) => new Vector3(v.x, v.y, 0);
}