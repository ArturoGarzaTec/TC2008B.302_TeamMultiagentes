using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRobot : MonoBehaviour
{
    float speed;
    public int priority;
    public GameObject[] Triggers;
    int[] rotY = {0, 90, 180, 270};
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.005f, 0.01f);
    }

    void Move()
    {
        
        if(transform.eulerAngles.y == 0)
        {
            transform.position += new Vector3(0, 0, speed);   
        }
        else if (transform.eulerAngles.y == 180)
        {
            transform.position += new Vector3(0, 0, -speed);   
        }
        else if (transform.eulerAngles.y == 90)
        {
            transform.position += new Vector3(speed, 0, 0);   
        }
        else if (transform.eulerAngles.y == 270)
        {
            transform.position += new Vector3(-speed, 0, 0);   
        }

    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Utilizar los triggers para detectar robots, tarimas, cajas o paredes
    
    // Si este robot detecta otro robot en su trigger derecho o izquierdo y el otro robot te detecta vise versa,
    // significa que hay una intersección, revisar prioridad y dejar pasar al de prioridad mas grande. Cuando el robot de prioridad menor deja de detectar al otro robot pot sus triggers, que siga avanzando.

    // Si este robot detecta otro robot en su trigger derecho y el otro robot tambien en su derecho o ambos en izquierda, significa que van en paralelo y no van a chocar, que sigan avanzando.

    // Si este robot detecta otro robot en su trigger del centro y el otro robot tambien en su centro, El de prioridad mas grande se va a la derecha y el mas chico a la izquierda

    // Si este robot detecta otro robot en su trigger del centro pero el otro robot no lo detecta indicando que este robot esta justo en frente del otro pero el otro va de manera perpendicular,
    // se va a esperar el robot para que siga avanzando el otro y se espera a que ya no lo detecte para poder seguir avanzando

    //private void OnTriggerEnter(Collider collision) 
    //{
    //    int rot;

    //    GameObject obj = collision.gameObject;
    //    Debug.Log(obj);

    //    while (true){
    //        rot = Random.Range(0, rotY.Length);
    //        if (rotY[rot] != transform.eulerAngles.y){
    //            break;
    //        }

    //    }
    //    transform.rotation = Quaternion.Euler(0, rotY[rot], 0);
    //}

    // private void OnCollisionStay(Collision collision){
    //     int rot;

    //     GameObject obj = collision.gameObject;
    //     Debug.Log(obj);

    //     while (true){
    //         rot = Random.Range(0, rotY.Length);
    //         if (rotY[rot] != transform.eulerAngles.y){
    //             break;
    //         }

    //     }
    //     transform.rotation = Quaternion.Euler(0, rotY[rot], 0);
    // }
}
