using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAbility : MonoBehaviour
{
    private static List<FallAbility> fallingObjects = new List<FallAbility>();

    public IEnumerator Fall()
    {
        fallingObjects.Add(this);

        float startTime = Time.time;
        while (!Physics.Raycast(transform.position, Vector3.down, 0.5f))
        {
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

        fallingObjects.Remove(this);
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
