using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public Transform endPoint;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if startPoint and endPoint are assigned
        if (startPoint != null && endPoint != null)
        {
            // Update the positions of the LineRenderer
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }
}
