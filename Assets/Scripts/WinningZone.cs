using System.Collections.Generic;
using UnityEngine;

public class WinningZone : MonoBehaviour
{
    public List<DiceColor> requiredSideColors;
    public List<bool> areSidesCorrect;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        UpdateSideCorrectnes();
        meshRenderer.enabled = !CheckWinningCondition();
    }

    private void UpdateSideCorrectnes()
    {
        for (int i = 0; i < 6; i++)
        {
            if (requiredSideColors[i] == DiceColor.None)
            {
                areSidesCorrect[i] = true;
            }
            else
            {
                areSidesCorrect[i] = DiceTop.Instance.sidesByNumber[i + 1].material.color == DiceColorUtilities.GetColor(requiredSideColors[i]);
            }
        }
    }

    public bool CheckWinningCondition()
    {
        bool value = true;

        foreach (bool side in areSidesCorrect)
        {
            if (!side)
            {
                return false;
            }
        }

        return value;
    }
}
