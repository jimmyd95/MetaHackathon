using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int threshold;




    void Start()
    {
        // for every constellation
        int limit = 0;
        if(limit<threshold){
            limit = threshold;
        }

        for (int i = 0; i < limit; i++)
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
