using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUpdates : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isGrabbed = false;
    private DynamicFloat floatingComponent;    
    // private Transform childTransform;

    void Start()
    {
        // Get the reference to the child's transform
        // childTransform = transform.GetChild(0); // Assuming the child is the first child of the parent
        // Debug.Log("Transform child: "+childTransform.name);
        floatingComponent = GetComponent<DynamicFloat>();
    }

    void Update()
    {
        // Check for changes in the child's position, rotation, or scale
        floatingComponent.enabled = isGrabbed ? false:true;


        // if (childTransform.hasChanged && !floatingComponent.enabled)
        // {
        //     // Update the parent's transform based on the child's position
        //     transform.position = childTransform.position;
        //     Debug.Log("Child Changed transform..."+ transform.name);

        //     // Optionally, you can update rotation and scale as well
        //     // transform.rotation = childTransform.rotation;
        //     // transform.localScale = childTransform.localScale;

        //     // Reset the child's hasChanged flag
        //     childTransform.hasChanged = false;
        // }


    }

        void OnMouseDown()
    {
        // Set isGrabbed to true when the object is clicked/grabbed
        // isGrabbed = !isGrabbed;
        Debug.Log(transform.name+" Clicked: "+isGrabbed);
    }


    void OnMouseUp()
    {
        // Set isGrabbed to false when the object is released
        // isGrabbed = false;
         isGrabbed = !isGrabbed;
    }
}
