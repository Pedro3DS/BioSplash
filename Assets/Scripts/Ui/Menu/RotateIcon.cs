using UnityEngine;


/// <summary>
/// A simple MonoBehaviour that can be attached to a UI icon to rotate it continuously.
/// Like Ping Pong animation, will rotate to a certain angle and then rotate back to the original position, creating a smooth oscillating effect.
/// </summary>
public class RotateIcon : MonoBehaviour
{
    void FixedUpdate()
    {
        // Rotate the icon back and forth between -15 and 15 degrees on the Z-axis
        float angle = Mathf.PingPong(Time.time * 30f, 30f) - 15f; // Adjust the speed and range as needed
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
