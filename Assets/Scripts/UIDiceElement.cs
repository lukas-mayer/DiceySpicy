using UnityEngine;
using UnityEngine.UI;

public class UIDiceElement : MonoBehaviour
{
    public Image diceColorImage;
    public Image checkImage;

    public void SetDiceColor(Color color)
    {
        diceColorImage.color = color;
    }

    public void UpdateCheck(bool check)
    {
        checkImage.enabled = check;
    }
}
