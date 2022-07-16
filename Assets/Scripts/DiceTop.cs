using System.Collections.Generic;
using UnityEngine;

public class DiceTop : MonoBehaviour
{
    public static DiceTop Instance { get; private set; }
    public Dictionary<Vector3, DiceSide> sides = new Dictionary<Vector3, DiceSide>();

    private void Awake()
    {
        Instance = this;

        sides.Add(Vector3.up, new DiceSide { number = 6, color = "white" });
        sides.Add(Vector3.down, new DiceSide { number = 1, color = "white" });
        sides.Add(Vector3.left, new DiceSide { number = 4, color = "white" });
        sides.Add(Vector3.right, new DiceSide { number = 3, color = "white" });
        sides.Add(Vector3.forward, new DiceSide { number = 5, color = "white" });
        sides.Add(Vector3.back, new DiceSide { number = 2, color = "white" });
    }

    private Vector3 GetSideUp()
    {
        Vector3 upSide = Vector3.zero;

        if (Vector3.Distance(Vector3.up, transform.up) < 0.01f)
        {
            upSide = Vector3.up;
        }
        else if (Vector3.Distance(Vector3.up, -transform.up) < 0.01f)
        {
            upSide = Vector3.down;
        }
        else
            if (Vector3.Distance(Vector3.up, transform.right) < 0.01f)
        {
            upSide = Vector3.right;
        }
        else
            if (Vector3.Distance(Vector3.up, -transform.right) < 0.01f)
        {
            upSide = Vector3.left;
        }
        else
            if (Vector3.Distance(Vector3.up, transform.forward) < 0.01f)
        {
            upSide = Vector3.forward;
        }
        else
            if (Vector3.Distance(Vector3.up, -transform.forward) < 0.01f)
        {
            upSide = Vector3.back;
        }

        return upSide;
    }

    public int GetNumber()
    {


        return sides[GetSideUp()].number;
    }

    public string GetColor()
    {
        return sides[GetSideUp()].color;
    }
}

public class DiceSide
{
    public int number;
    public string color;
}
