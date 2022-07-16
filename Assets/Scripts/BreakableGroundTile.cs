using UnityEngine;

public class BreakableGroundTile : MonoBehaviour
{
    public int lifes = 1;

    public void OnContact()
    {
        PlayerMovement.Instance.OnMove.AddListener(OnExit);
    }

    public void OnExit()
    {
        lifes--;
        if (lifes == 0)
        {
            Break();
        }

        UndoManager.Instance.undoStack.Peek().breakableGroundTile = this;
    }

    private void Break()
    {
        gameObject.SetActive(false);
    }

    public void Undo()
    {
        if (lifes == 0)
        {
            gameObject.SetActive(true);
        }

        lifes++;
    }

    private void OnDisable()
    {
        PlayerMovement.Instance.OnMove.RemoveListener(OnExit);
    }
}
