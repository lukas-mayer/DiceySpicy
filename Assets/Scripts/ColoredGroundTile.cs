using UnityEngine;

public class ColoredGroundTile : MonoBehaviour
{
    public DiceColor color;
    public int lifes = 1;

    public void OnContact()
    {
        SetColorSide(DiceTop.Instance.GetBottomNumber());
    }

    public void Undo()
    {
        if (lifes == 0)
        {
            gameObject.SetActive(true);
        }

        lifes++;
    }

    private void SetColorSide(int number)
    {


        switch (number)
        {
            case 1:
                DiceTop.Instance.material1.color = GetColor(color);
                break;
            case 2:
                DiceTop.Instance.material2.color = GetColor(color);
                break;
            case 3:
                DiceTop.Instance.material3.color = GetColor(color);
                break;
            case 4:
                DiceTop.Instance.material4.color = GetColor(color);
                break;
            case 5:
                DiceTop.Instance.material5.color = GetColor(color);
                break;
            case 6:
                DiceTop.Instance.material6.color = GetColor(color);
                break;

        }
    }

    private Color GetColor(DiceColor diceColor)
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
    Blue,
    Green,
    White
}
