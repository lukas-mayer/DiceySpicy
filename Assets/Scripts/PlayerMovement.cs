using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FallAbility))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField] private float rotationSpeed = 150;
    public UnityEvent OnMove;


    public bool IsMoving => isMoving;

    private bool isMoving = false;
    private float currentAngle;
    private float maxAngle;
    private Vector3 rotationOffset;
    private Vector3 rotationAxis;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private MainCamera cam;
    public FallAbility fallAbility;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main.GetComponent<MainCamera>();
        fallAbility = GetComponent<FallAbility>();
    }


    public void MoveForward()
    {
        Move(cam.GetForward());
    }

    public void MoveBack()
    {
        Move(cam.GetBack());
    }

    public void MoveLeft()
    {
        Move(cam.GetLeft());
    }

    public void MoveRight()
    {
        Move(cam.GetRight());
    }

    public void Move(Vector3 direction, bool undoable = true)
    {
        if (undoable)
        {
            UndoManager.Instance.AddMove(-direction);
        }

        StartCoroutine(RotateAroundAxis(direction, undoable));
    }

    private IEnumerator RotateAroundAxis(Vector3 direction, bool undoable)
    {
        if (!CanMove(direction))
        {
            yield break;
        }

        isMoving = true;

        if (undoable)
        {
            OnMove?.Invoke();
        }
        else
        {
            OnMove.RemoveAllListeners();
        }

        CalculateRotationData(direction);

        while (currentAngle < maxAngle)
        {
            transform.RotateAround(initialPosition + rotationOffset,
                rotationAxis, rotationSpeed * Time.deltaTime);
            currentAngle += rotationSpeed * Time.deltaTime;
            yield return null;
        }

        transform.SetPositionAndRotation(transform.position.Round(), initialRotation);
        transform.Rotate(rotationAxis, maxAngle, Space.World);


        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            if (hit.transform.gameObject.GetComponent<WinningZone>() && hit.transform.gameObject.GetComponent<WinningZone>().CheckWinningCondition())
            {
                print("LVL FINISHED!");
                cam.GetComponent<MainCamera>().ignoreYMovement = false;
                cam.transform.parent = transform;
                StartCoroutine(fallAbility.FallToNextStage());
            }

            hit.transform.gameObject.GetComponent<BreakableGroundTile>()?.OnContact();

            if (undoable)
            {
                hit.transform.gameObject.GetComponent<ColoredGroundTile>()?.OnContact();
            }
        }
        else
        {
            StartCoroutine(fallAbility.Fall());
        }

        yield return new WaitWhile(FallAbility.AreObjectsFalling);

        isMoving = false;
    }

    private bool CanMove(Vector3 direction)
    {
        return true;
    }

    private void CalculateRotationData(Vector3 direction)
    {
        rotationAxis = direction.RotateAroundY(90);
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        currentAngle = 0;

        CalculateMaxAngle(direction);
        CalculateRotationOffset(direction);
    }

    private void CalculateRotationOffset(Vector3 direction)
    {
        rotationOffset = (Vector3.down + direction) / 2;

        /*Transform rotatee = transform;

        if (rotatee != null)
        {
            rotationOffset = rotatee.position - transform.position + direction / 2 + Vector3.up / 2;

        }
        else if (!firstChildren.Any(t => Physics.Raycast(t.position, Vector3.down, 1f)) &&
            !firstChildren.Any(t => Physics.Raycast(t.position + Vector3.down, direction, 1f)))
        {
            rotationOffset -= direction;
        }*/
    }

    private void CalculateMaxAngle(Vector3 direction)
    {
        maxAngle = 90;
    }
}
