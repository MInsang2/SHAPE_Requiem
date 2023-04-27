using UnityEngine;

public class test : MonoBehaviour
{
    public Transform target; // The point to converge towards
    public float rotationSpeed = 10f; // The speed at which the object rotates
    public float convergenceSpeed = 1f; // The speed at which the object converges towards the target

    void Update()
    {
        // Rotate the object around its z-axis
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

        // Move the object towards the target
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * convergenceSpeed);
    }
}

