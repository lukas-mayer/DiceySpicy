using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public static UndoManager Instance;
    public PlayerMovement movement;

    public Stack<StackData> undoStack = new Stack<StackData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddMove(Vector3 direction)
    {
        undoStack.Push(new StackData(direction));
    }


    public IEnumerator Undo()
    {
        if (undoStack.Count > 0)
        {
            StackData stackData = undoStack.Pop();

            if (stackData.fallAbility != null)
            {
                yield return stackData.fallAbility.Unfall();
            }

            stackData.coloredGroundTile?.Undo();
            stackData.breakableGroundTile?.Undo();
            movement.Move(stackData.direction, undoable: false);
        }
    }
}

public class StackData
{
    public Vector3 direction;
    public BreakableGroundTile breakableGroundTile;
    public FallAbility fallAbility;
    public ColoredGroundTile coloredGroundTile;

    public StackData(Vector3 direction)
    {
        this.direction = direction;
    }
}
