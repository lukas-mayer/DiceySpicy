using UnityEngine;

public class UndoHint : MonoBehaviour
{
    public static UndoHint Instance;
    private void Awake()
    {
        gameObject.SetActive(false);
        Instance = this;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
