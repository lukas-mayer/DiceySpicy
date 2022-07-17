using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    public static CameraAngle Instance { get; private set; }

    public Quaternion quaternion;
    public Vector3? offset = null;
    public float angle = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            quaternion = Camera.main.transform.rotation;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
