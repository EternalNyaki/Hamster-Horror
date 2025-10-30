using System;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Camera defaultCamera;

    public event Action<Camera> cameraTriggeredEvent;

    private Camera m_mainCamera = null;

    protected override void Initialize()
    {
        base.Initialize();

        cameraTriggeredEvent += SetMainCamera;

        SetMainCamera(defaultCamera);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCameraTriggeredEvent(Camera camera)
    {
        cameraTriggeredEvent?.Invoke(camera);
    }

    private void SetMainCamera(Camera camera)
    {
        if (m_mainCamera != null)
        {
            m_mainCamera.enabled = false;
            m_mainCamera.GetComponent<AudioListener>().enabled = false;
        }

        camera.enabled = true;
        camera.GetComponent<AudioListener>().enabled = true;

        m_mainCamera = camera;
    }
}
