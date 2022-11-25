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
        obj = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        obj = null;
    }
}
