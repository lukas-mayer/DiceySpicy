using UnityEngine;

public class ColoredGroundTile : MonoBehaviour
{
    public MeshRenderer colorSpash;
    public DiceColor color;

    private void Awake()
    {
        Material colorSplashMaterial = colorSpash.material;
        colorSplashMaterial.color = DiceColorUtilities.GetColor(color);
    }

    public void OnContact()
    {
        SetColorSide(DiceTop.Instance.GetBottomNumber());
        UndoManager.Instance.undoStack.Peek().coloredGroundTile = this;
    }

    public void Undo()
    {
        ResetColorSide(DiceTop.Instance.GetBottomNumber());
    }

    private void ResetColorSide(int number)
    {
        DiceSide side = DiceTop.Instance.sidesByNumber[number];

        side.material.color = side.colorHistory.Pop();
    }

    private void SetColorSide(int number)
    {
        DiceSide side = DiceTop.Instance.sidesByNumber[number];
        side.colorHistory.Push(side.material.color);
        side.material.color = DiceColorUtilities.GetColor(color);
    }
}