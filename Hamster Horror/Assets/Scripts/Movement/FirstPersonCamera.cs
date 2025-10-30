using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 positionOffset;
    public float angleOffset;

    public bool lockRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!lockRotation)
        {
            float yaw = Mathf.Deg2Rad * (target.rotation.eulerAngles.y + angleOffset);
            float x = positionOffset.x * Mathf.Sin(yaw) - positionOffset.z * Mathf.Cos(yaw);
            float z = positionOffset.x * Mathf.Cos(yaw) + positionOffset.z * Mathf.Sin(yaw);
            Vector3 realOffset = new Vector3(x, positionOffset.y, z);
            transform.position = target.position + realOffset;
            transform.rotation = Quaternion.AngleAxis(target.rotation.eulerAngles.y + angleOffset, Vector3.up);
        }
        else
        {
            transform.position = target.position + positionOffset;
            transform.rotation = Quaternion.Euler(0f, angleOffset, 0f);
        }
    }
}
