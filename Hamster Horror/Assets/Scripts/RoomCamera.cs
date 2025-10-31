using ParadoxNotion;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public LayerMask playerLayer;

    private Transform m_lookTarget;

    private Camera m_camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        m_camera.transform.LookAt(m_lookTarget);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.IsInLayerMask(playerLayer))
        {
            CameraManager.Instance.OnCameraTriggeredEvent(m_camera);
            m_lookTarget = other.transform;
        }
    }
}
