using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAbility : MonoBehaviour
{
    private static List<FallAbility> fallingObjects = new List<FallAbility>();

    public Vector3 startFallPosition = Vector3.zero;
    public float maxSpeed = 40f;

    public Stack<Vector3> positions = new Stack<Vector3>();
    public bool isFalling = false;

    private void Start()
    {
        Camera.main.GetComponent<MainCamera>().ignoreYMovement = false;
        Camera.main.transform.parent = transform;
        StartCoroutine(LandInStage());
    }

    public IEnumerator FallToNextStage()
    {
        isFalling = true;
        float startTime = Time.time;
        float fallingSpeed = 0;
        while (transform.position.y > -20)
        {
            fallingSpeed = Mathf.Clamp(fallingSpeed + Time.deltaTime * 20f, 0, maxSpeed);
            transform.Translate(fallingSpeed * Time.deltaTime * Vector3.down, Space.World);
            yield return null;
        }

        SceneLoader.LoadNextScene();
    }

    public IEnumerator LandInStage()
    {

        isFalling = true;
        fallingObjects.Add(this);
        float startTime = Time.time;
        while (!Physics.Raycast(transform.position, Vector3.down, 0.5f))
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

        isFalling = false;
        Camera.main.GetComponent<MainCamera>().ignoreYMovement = true;
        Camera.main.transform.parent = null;
        fallingObjects.Remove(this);
    }

    public IEnumerator Fall()
    {
        fallingObjects.Add(this);

        startFallPosition = transform.position;

        float startTime = Time.time;
        while (!Physics.Raycast(transform.position, Vector3.down, 0.5f) && Time.time - startTime < 1.5)
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
