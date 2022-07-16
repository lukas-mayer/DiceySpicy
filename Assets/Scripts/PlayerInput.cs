using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    private MainCamera cam;

    [SerializeField] private float timeScaleFactor;
    private float undoStartTime = 0f;

    private void Awake()
    {
        cam = Camera.main.GetComponent<MainCamera>();
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.R))
        {
            undoStartTime = 0;
            Time.timeScale = 1;
        }
        else if (undoStartTime != 0 && Time.time - undoStartTime > 1)
        {
            print(Time.timeScale);
            Time.timeScale = Mathf.Clamp(Time.timeScale + timeScaleFactor * Time.deltaTime, 1, 5);
        }


        if (movement.IsMoving || cam.IsRotating)
        {
            return;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            movement.MoveForward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.MoveBack();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.MoveRight();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(cam.RotateRight());
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(cam.RotateLeft());
        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (undoStartTime == 0)
            {
                undoStartTime = Time.time;
            }

            UndoManager.Instance.Undo();
        }
    }
}
