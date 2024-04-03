using System.Collections;
using UnityEngine;

public class GirarCuerpo : MonoBehaviour
{
    public float tiltRange = 3.5f; // Range of tilt from the initial angle
    private float tiltDuration = 0.5f; // Duration to tilt from one side to the other


    void Start()
    {
        StartCoroutine(TiltRoutine());
    }

    IEnumerator TiltRoutine()
    {
        // Calculate the maximum and minimum angles based on the tilt range
        float minAngle = transform.eulerAngles.z - tiltRange;
        float maxAngle = transform.eulerAngles.z + tiltRange;

        while (true)
        {
            // Tilt to the minimum angle
            yield return StartCoroutine(TiltOverTime(minAngle, tiltDuration));
            // Tilt to the maximum angle
            yield return StartCoroutine(TiltOverTime(maxAngle, tiltDuration));
        }
    }

    IEnumerator TiltOverTime(float targetAngle, float duration)
    {
        float time = 0;
        float startAngle = transform.eulerAngles.z;
        startAngle = NormalizeAngle(startAngle);
        targetAngle = NormalizeAngle(targetAngle);

        while (time < duration)
        {
            float angle = Mathf.LerpAngle(startAngle, targetAngle, time / duration);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
    }

    // Ensure the angle is within -180 to 180 for correct lerping
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180)
            return angle - 360;
        if (angle < -180)
            return angle + 360;
        return angle;
    }
}
