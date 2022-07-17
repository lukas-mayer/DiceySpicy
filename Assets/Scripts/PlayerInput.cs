using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    [SerializeField] private PlayerMovement movement;
    private MainCamera cam;
    public bool isAlive = true;

    [SerializeField] private float timeScaleFactor;
    private float undoStartTime = 0f;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main.GetComponent<MainCamera>();
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.R))
        {
            undoStartTime = -1;
            Time.timeScale = 1;
        }
        else if (undoStartTime != -1 && Time.time - undoStartTime > 1)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + timeScaleFactor * Time.deltaTime, 1, 5);
        }

        if (movement.IsMoving || cam.IsRotating || movement.fallAbility.isFalling)
        {
            return;
        }
        else if (isAlive)
        {
            if (Input.GetKey(KeyCode.W))
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
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (undoStartTime == -1)
            {
                undoStartTime = Time.time;
            }
            isAlive = true;
            StartCoroutine(UndoManager.Instance.Undo());
        }
    }
}
