using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public GameObject[] Robots;
    public Vector3[] positionsArray;
    int[] rotY= {0, 90, 180, 270};
    List<int> usedPos=new List<int>();
    int rP = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject MyRobot in Robots){
            //while (true){
            //    rP = Random.Range(0, positionsArray.Length - 1);
            //    if (!usedPos.Contains(rP)){
            //        usedPos.Add(rP);
            //        break;
            //    }

            //}
            //int rR = Random.Range(0, rotY.Length - 1);
            int rR = 0;

            MyRobot.transform.SetPositionAndRotation(positionsArray[rP], Quaternion.Euler(0, rotY[rR], 0));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
