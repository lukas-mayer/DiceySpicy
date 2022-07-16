using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAbility : MonoBehaviour
{
    private static List<FallAbility> fallingObjects = new List<FallAbility>();

    public Vector3 startFallPosition = Vector3.zero;
    public float maxSpeed = 20f;

    public Stack<Vector3> positions = new Stack<Vector3>();
    public bool isFalling = false;

    public IEnumerator FallToNextStage()
    {
        print("Fall!");
        isFalling = true;
        float startTime = Time.time;
        float fallingSpeed = 0;
        while (isFalling)
        {
            fallingSpeed = Mathf.Clamp(fallingSpeed + Time.deltaTime * 3f, 0, maxSpeed);
            transform.Translate(Vector3.down * Time.deltaTime * fallingSpeed, Space.World);
            yield return null;
        }
    }

    public IEnumerator Fall(bool ignoreGround = false)
    {
        fallingObjects.Add(this);

        startFallPosition = transform.position;

        float startTime = Time.time;
        while (!Physics.Raycast(transform.position, Vector3.down, 0.5f) && Time.time - startTime < 1)
        {
            positions.Push(transform.position);
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (0.25f * Mathf.Pow(Time.time - startTime, 2)),
                transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(
                transform.position.x,
                Mathf.Round(transform.position.y),
                transform.position.z);

        if (Time.time - startTime >= 1)
        {
            PlayerInput.Instance.isAlive = false;
        }
        UndoManager.Instance.undoStack.Peek().fallAbility = this;

        fallingObjects.Remove(this);
    }

    public IEnumerator Unfall()
    {
        isFalling = true;
        while (transform.position != startFallPosition)
        {
            transform.position = positions.Pop();
            yield return null;
        }
        isFalling = false;
    }

    public static bool AreObjectsFalling()
    {
        if (fallingObjects.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
