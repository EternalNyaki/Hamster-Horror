using ParadoxNotion;
using UnityEngine;

public class DogAIEventHandler : MonoBehaviour
{
    public LayerMask visionLayers;
    public LayerMask blockVisionLayers;
    public float fieldOfView;

    public event System.Action<Transform> playerSpottedEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        // Check if collider's layer in within the layer mask
        if (!other.gameObject.IsInLayerMask(visionLayers)) { return; }

        //Check line of sight to collider
        Vector3 vectorToOther = other.transform.position - transform.position;
        float angleToOther = Mathf.Rad2Deg * Mathf.Atan2(vectorToOther.z - transform.forward.z, vectorToOther.x - transform.forward.x);
        if (Mathf.Abs(angleToOther) < fieldOfView / 2)
        {
            if (!Physics.Raycast(transform.position, vectorToOther, vectorToOther.magnitude, blockVisionLayers))
            {
                OnPlayerSpottedEvent(other.transform);
                Debug.DrawLine(transform.position, other.transform.position, Color.green);
            }
            else
            {
                Debug.DrawLine(transform.position, other.transform.position, Color.yellow);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + vectorToOther, Color.red);
        }
    }

    private void OnPlayerSpottedEvent(Transform player)
    {
        playerSpottedEvent?.Invoke(player);
    }
}
