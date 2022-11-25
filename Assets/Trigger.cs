using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject trigger;
    public GameObject obj;
    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        isTriggered = true;

        if (trigger.tag == "CenterTrigger")
        {
            Debug.Log("CenterTrigger");
        }
        else if (trigger.tag == "RightTrigger")
        {
            Debug.Log("RightTrigger");
        }
        else if (trigger.tag == "LeftTrigger")
        {
            Debug.Log("LeftTrigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
