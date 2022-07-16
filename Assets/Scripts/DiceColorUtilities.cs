using UnityEngine;

public static class DiceColorUtilities
{
    public static Color GetColor(DiceColor diceColor)
    {
        switch (diceColor)
        {
            case DiceColor.Blue: return Color.blue;
            case DiceColor.Green: return Color.green;
            case DiceColor.White: return Color.white;
            default:
                break;
        }
        return Color.white;
    }
}

public enum DiceColor
{
    None,
    Blue,
    Green,
    White
}
