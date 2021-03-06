using System.Collections;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private float rotationSpeed = 1;
    private new Transform transform;
    private Vector3 offset;
    private float distanceToPlayer;
    private bool isRotating = false;

    public bool ignoreYMovement = true;
    public bool IsRotating => isRotating;

    public float currentAngle = 0;

    private void Awake()
    {
        transform = GetComponent<Transform>();


    }

    private void Start()
    {
        if (CameraAngle.Instance.offset == null)
        {
            offset = transform.position - player.position;
        }
        else
        {
            offset = (Vector3)CameraAngle.Instance.offset;
        }

        currentAngle = CameraAngle.Instance.angle;

        distanceToPlayer = Vector3.Magnitude(offset.Flaten());
        transform.position = player.position + offset;
        transform.rotation = CameraAngle.Instance.quaternion;
    }

    private void LateUpdate()
    {
        if (ignoreYMovement)
        {
            transform.position = player.position.Flaten() + offset;
        }
    }

    public IEnumerator RotateRight()
    {
        isRotating = true;

        Quaternion initialRotation = transform.rotation;
        float angle = currentAngle;
        while (angle < 90 + currentAngle)
        {
            angle += rotationSpeed * Time.deltaTime;
            offset = ClaculateNewOffset(angle);
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        currentAngle = RemoveOvershoot(angle) % 360;
        offset = ClaculateNewOffset(currentAngle);
        transform.rotation = initialRotation;
        transform.Rotate(Vector3.up, -90, Space.World);

        CameraAngle.Instance.angle = currentAngle;
        CameraAngle.Instance.offset = offset;
        CameraAngle.Instance.quaternion = transform.rotation;
        isRotating = false;
    }

    public IEnumerator RotateLeft()
    {
        isRotating = true;

        Quaternion initialRotation = transform.rotation;
        float angle = currentAngle;
        while (angle > -90 + currentAngle)
        {
            angle -= rotationSpeed * Time.deltaTime;
            offset = ClaculateNewOffset(angle);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        currentAngle = RemoveOvershoot(angle) % 360;
        offset = ClaculateNewOffset(currentAngle);
        transform.rotation = initialRotation;
        transform.Rotate(Vector3.up, 90, Space.World);

        CameraAngle.Instance.angle = currentAngle;
        CameraAngle.Instance.offset = offset;
        CameraAngle.Instance.quaternion = transform.rotation;
        isRotating = false;
    }

    public Vector3 GetForward()
    {

        return Vector3.forward.RotateAroundY(-currentAngle);
    }

    public Vector3 GetBack()
    {
        return Vector3.back.RotateAroundY(-currentAngle);
    }

    public Vector3 GetLeft()
    {
        return Vector3.left.RotateAroundY(-currentAngle);
    }

    public Vector3 GetRight()
    {
        return Vector3.right.RotateAroundY(-currentAngle);
    }

    private float RemoveOvershoot(float original)
    {
        original = Mathf.Floor(original);
        float remainder = original % 90;
        var back = original - remainder;
        if (remainder > 45)
        {
            back += 90;
        }
        else if (remainder < -45)
        {
            back -= 90;
        }

        return back;
    }

    private Vector3 ClaculateNewOffset(float angle)
    {
        return new Vector3(
                distanceToPlayer * Mathf.Sin(angle.ToRadiant()),
                offset.y,
                distanceToPlayer * -Mathf.Cos(angle.ToRadiant()));
    }
}
