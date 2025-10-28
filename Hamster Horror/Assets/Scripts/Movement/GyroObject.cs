using UnityEngine;

public class GyroObject : MonoBehaviour
{
    public float speed = 1 / 90;

    private Joycon m_joycon;

    private Rigidbody m_rigidbody;

    [SerializeField] private Vector3 m_movementVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        if (JoyconManager.Instance.j.Count <= 0)
        {
            Debug.LogError("No joycon connected");
            Destroy(gameObject);
        }

        m_joycon = JoyconManager.Instance.j[0];
        m_joycon.Recenter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f) * m_joycon.GetVector();

        float theta = Mathf.Deg2Rad * transform.rotation.eulerAngles.y;
        float gyroX = m_joycon.GetGyro().x;
        float gyroZ = -m_joycon.GetGyro().y;
        float x = gyroX * Mathf.Cos(theta) - gyroZ * Mathf.Sin(theta);
        float y = gyroX * Mathf.Sin(theta) + gyroZ * Mathf.Cos(theta);
        Vector2 velocityOverGround = new Vector2(x, y) * speed;
        Vector3 velocity = new(velocityOverGround.x, m_rigidbody.linearVelocity.y, velocityOverGround.y);
        m_rigidbody.linearVelocity = velocity;
    }
}
