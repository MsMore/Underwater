using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    // The limits on the x, y, and z axes
    private float xMin = -50.1f;
    private float xMax = 43.3f;
    private float yMin = 0f;
    private float yMax = 27f;
    private float zMin = -19.8f;
    private float zMax = 19f;

    private SubmarineController submarine;

    void Update()
    {
        // Clamp the position of the game object to the specified limits
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, xMin, xMax),
            Mathf.Clamp(transform.localPosition.y, yMin, yMax),
            Mathf.Clamp(transform.localPosition.z, zMin, zMax));
    }
}
