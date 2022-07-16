using System;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public static UndoManager Instance;
    public PlayerMovement movement;

    public Stack<StackData> undoStack = new Stack<StackData>();

    public Stack<Stack<Action<Vector3>>> undos = new Stack<Stack<Action<Vector3>>>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddMove(Vector3 direction)
    {
        undoStack.Push(new StackData(direction, null));
    }


    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            StackData stackData = undoStack.Pop();


            stackData.breakableGroundTile?.Undo();
            movement.Move(stackData.direction, undoable: false);
        }
    }
}

public class StackData
{
    public Vector3 direction;
    public BreakableGroundTile breakableGroundTile;

    public StackData(Vector3 direction, BreakableGroundTile breakableGroundTile)
    {
        this.direction = direction;
        this.breakableGroundTile = breakableGroundTile;
    }
}
