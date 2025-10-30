using System.Collections;
using TMPro;
using UnityEngine;

public class QTETesting : MonoBehaviour
{
    public GyroObject gyroObject;
    public TMP_Text uiText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Start QTE
            uiText.text = "Struggle to freedom";
            StartCoroutine(PlayQTE(5 * 360f, 5f));
        }
    }

    private IEnumerator PlayQTE(float difficulty, float timeLimit)
    {
        float struggleCounter = 0f;
        float timer = 0f;

        while (timer < timeLimit)
        {
            gyroObject.GetRotationDelta().ToAngleAxis(out float angle, out Vector3 axis);
            float smoothAngle = angle < 360 - angle ? angle : 360 - angle;
            struggleCounter += Mathf.Abs(smoothAngle);

            if (struggleCounter > difficulty)
            {
                uiText.text = "Survived";
                yield break;
            }

            yield return null;

            timer += Time.deltaTime;
        }

        uiText.text = "You died";
    }
}
