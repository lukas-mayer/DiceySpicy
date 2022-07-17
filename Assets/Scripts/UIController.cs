using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public List<UIDiceElement> dice = new List<UIDiceElement>();
    private WinningZone winningZone;

    private void Start()
    {
        winningZone = WinningZone.Instance;
        Setup();
    }

    private void Setup()
    {
        for (int i = 0; i < dice.Count; i++)
        {
            if (winningZone.requiredSideColors[i] == DiceColor.None)
            {
                dice[i].gameObject.SetActive(false);
            }
            else
            {
                dice[i].diceColorImage.color = DiceColorUtilities.GetColor(winningZone.requiredSideColors[i]);
            }
        }
    }

    private void CheckRequirements()
    {
        for (int i = 0; i < dice.Count; i++)
        {
            dice[i].UpdateCheck(winningZone.areSidesCorrect[i]);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CheckRequirements();
    }
}