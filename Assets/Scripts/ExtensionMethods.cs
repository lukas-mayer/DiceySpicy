using UnityEngine;

public static class ExtensionMethods
{
    public static Vector3 Flaten(this Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }

    public static Vector3 RotateAroundY(this Vector3 vec, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotation);
        return rotationMatrix.MultiplyVector(vec);
    }

    public static Vector3 Round(this Vector3 vec)
    {
        vec.x = Mathf.Round(vec.x);
        vec.y = Mathf.Round(vec.y);
        vec.z = Mathf.Round(vec.z);
        return vec;
    }

    public static float ToRadiant(this float degrees)
    {
        return degrees * Mathf.PI / 180;
    }

    public static bool IsApproximately(this float a, float b)
    {
        return Mathf.Round(a) == Mathf.Round(b);
    }
}
