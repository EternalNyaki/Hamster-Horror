using UnityEngine;

public class GyroObject : MonoBehaviour
{
    public float speed = 1 / 90;
    public Animator animator;
    public GameObject hamster;
    private Joycon m_joycon;

    private Rigidbody m_rigidbody;

    private Quaternion m_prevOrientation;

    [SerializeField] private Vector3 m_movementVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get component references
        m_rigidbody = GetComponent<Rigidbody>();

        if (JoyconManager.Instance.j.Count <= 0)
        {
            Debug.LogError("No joycon connected");
            Destroy(gameObject);
        }

        //Get joycon reference
        m_joycon = JoyconManager.Instance.j[0];
        m_joycon.Recenter();

        animator = hamster.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("runSpeed", m_rigidbody.linearVelocity.magnitude);

        if (m_rigidbody.linearVelocity.x >= 1f || m_rigidbody.linearVelocity.y >= 1f || m_rigidbody.linearVelocity.x <= -1f || m_rigidbody.linearVelocity.y <= -1f)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }

    }

    void FixedUpdate()
    {
        //Set velocity and orientation
        m_rigidbody.linearVelocity = GetVelocityQuaternionMethod();
        transform.rotation = GetTrueOrientation();

        hamster.transform.position = transform.position;
        hamster.transform.rotation = Quaternion.Euler(hamster.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, hamster.transform.rotation.eulerAngles.z);

        m_prevOrientation = GetTrueOrientation();

    }

    /// <summary>
    /// Corrects the joycon orientation by rotating it 90 degrees about the x-axis
    /// </summary>
    /// <returns></returns>
    public Quaternion GetTrueOrientation()
    {
        return Quaternion.Euler(90f, 0f, 0f) * m_joycon.GetVector();
    }

    public Quaternion GetRelativeOrientation()
    {
        return Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * GetTrueOrientation();
    }

    public Quaternion GetRotationDelta()
    {
        return GetTrueOrientation() * Quaternion.Inverse(m_prevOrientation);
    }

    private Vector3 GetVelocityQuaternionMethod()
    {
        //Get change in rotation as a quaternion
        Quaternion delta = GetTrueOrientation() * Quaternion.Inverse(m_prevOrientation);

        delta.ToAngleAxis(out float angle, out Vector3 axis);
        float smoothAngle = angle < 360 - angle ? angle : 360 - angle;
        Vector3 groundNormal = Vector3.up;

        //Cross product of axis of rotation and normal vector with the ground
        Vector3 direction = Vector3.Cross(axis, groundNormal);
        Vector3 velocityOverGround = direction * smoothAngle * speed;

        //HACK: Currently hard-coded for flat ground
        return new(velocityOverGround.x, m_rigidbody.linearVelocity.y, velocityOverGround.z);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (m_movementVector / Time.fixedDeltaTime));
    }
#endif
}
