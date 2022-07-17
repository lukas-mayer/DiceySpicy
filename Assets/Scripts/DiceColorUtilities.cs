using UnityEngine;

public static class DiceColorUtilities
{
    public static Color GetColor(DiceColor diceColor)
    {
        Color color;

        switch (diceColor)
        {
            case DiceColor.Blue:
                ColorUtility.TryParseHtmlString("#2B39DB", out color);
                break;
            case DiceColor.Green:
                ColorUtility.TryParseHtmlString("#3AEB51", out color);
                break;
            case DiceColor.Red:
                ColorUtility.TryParseHtmlString("#EB2923", out color);
                break;
            case DiceColor.Yellow:
                ColorUtility.TryParseHtmlString("#DBCA1D", out color);
                break;
            default:
                color = Color.white;
                break;
        }
        return color;
    }
}

public enum DiceColor
{
    None,
    Blue,
    Green,
    White,
    Red,
    Yellow,
}
