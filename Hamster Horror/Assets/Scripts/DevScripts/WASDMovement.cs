using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody m_rigidbody;

    private Vector2 m_input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        m_input = new Vector2(x, y).normalized * speed;
    }

    void FixedUpdate()
    {
        m_rigidbody.linearVelocity = new Vector3(m_input.x, m_rigidbody.linearVelocity.y, m_input.y);
    }
}
