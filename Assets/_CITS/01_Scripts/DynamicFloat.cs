using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFloat : MonoBehaviour
{
    public float floatHeight = 1f; // The maximum height of the float
    public float floatSpeed = 1f; // The speed of the float
    [SerializeField] private bool enabled=true;
    [SerializeField] private bool useFixedCoordinates=false;
    public Vector3 starCoordinate;

    private Rigidbody rb;
    private float startY; // Initial Y position
    private Vector3 originalPosition; // The original position of the object

    void Start(){
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the vertical offset using a sine function
        if(enabled){
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Update the object's position with the vertical offset
        transform.position = originalPosition + new Vector3(0f, yOffset, 0f);
        }
        if(useFixedCoordinates){
            transform.position = starCoordinate; // set the value to its fixed coordinates
        }

    }

}
