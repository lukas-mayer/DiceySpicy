using System.Collections.Generic;
using UnityEngine;

public class DiceTop : MonoBehaviour
{
    public static DiceTop Instance { get; private set; }
    public Dictionary<Vector3, DiceSide> sidesByDirection = new Dictionary<Vector3, DiceSide>();
    public Dictionary<int, DiceSide> sidesByNumber = new Dictionary<int, DiceSide>();

    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    public Material material6;

    private void Awake()
    {
        Instance = this;

        DiceSide side1 = new DiceSide(1, material1);
        DiceSide side2 = new DiceSide(2, material2);
        DiceSide side3 = new DiceSide(3, material3);
        DiceSide side4 = new DiceSide(4, material4);
        DiceSide side5 = new DiceSide(5, material5);
        DiceSide side6 = new DiceSide(6, material6);

        sidesByDirection.Add(Vector3.up, side6);
        sidesByDirection.Add(Vector3.down, side1);
        sidesByDirection.Add(Vector3.left, side4);
        sidesByDirection.Add(Vector3.right, side3);
        sidesByDirection.Add(Vector3.forward, side5);
        sidesByDirection.Add(Vector3.back, side2);

        sidesByNumber.Add(6, side6);
        sidesByNumber.Add(1, side1);
        sidesByNumber.Add(4, side4);
        sidesByNumber.Add(3, side3);
        sidesByNumber.Add(5, side5);
        sidesByNumber.Add(2, side2);
    }

    private void OnDestroy()
    {
        material1.color = Color.white;
        material2.color = Color.white;
        material3.color = Color.white;
        material4.color = Color.white;
        material5.color = Color.white;
        material6.color = Color.white;
    }


    private Vector3 GetSideUp()
    {
        Vector3 upSide = Vector3.zero;

        if (Vector3.Distance(Vector3.up, transform.up) < 0.01f)
        {
            upSide = Vector3.back;
        }
        else if (Vector3.Distance(Vector3.up, -transform.up) < 0.01f)
        {
            upSide = Vector3.forward;
        }
        else
            if (Vector3.Distance(Vector3.up, transform.right) < 0.01f)
        {
            upSide = Vector3.down;
        }
        else
            if (Vector3.Distance(Vector3.up, -transform.right) < 0.01f)
        {
            upSide = Vector3.up;
        }
        else
            if (Vector3.Distance(Vector3.up, transform.forward) < 0.01f)
        {
            upSide = Vector3.right;
        }
        else
            if (Vector3.Distance(Vector3.up, -transform.forward) < 0.01f)
        {
            upSide = Vector3.left;
        }

        return upSide;
    }

    public int GetNumber()
    {
        return sidesByDirection[GetSideUp()].number;
    }

    public int GetBottomNumber()
    {
        return 7 - GetNumber();
    }

    public Color GetColor()
    {
        return sidesByDirection[GetSideUp()].material.color;
    }
}

public class DiceSide
{
    public int number;
    public Material material;
    public Stack<Color> colorHistory;

    public DiceSide(int number, Material material)
    {
        this.number = number;
        this.material = material;
        colorHistory = new Stack<Color>();
    }
}
