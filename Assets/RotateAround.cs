using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target; // Transform of the object to orbit around
    public float orbitRadius; // Radius of the orbit
    public float rotationSpeed; // Speed of rotation (degrees per second)

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the angle of rotation
        float angle = Time.time * rotationSpeed;

        // Convert angle to radians
        angle *= Mathf.Deg2Rad;

        // Calculate the orbiting position
        Vector3 position = target.position + new Vector3(Mathf.Cos(angle) * orbitRadius, Mathf.Sin(angle) * orbitRadius, 0f);

        // Set the object's position
        transform.position = position;

        // Rotate the object to face the target
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(position.y - target.position.y, position.x - target.position.x) * Mathf.Rad2Deg - 90f);
    }
}
