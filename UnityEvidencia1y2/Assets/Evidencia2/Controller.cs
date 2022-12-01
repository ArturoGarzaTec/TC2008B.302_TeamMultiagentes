using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class AgentData
{
    public int id, type;
    public float x, y, z;
    public bool edge;

    public AgentData(int id, int type, float x, float y, float z)
    {
        this.id = id;
        this.type = type;
        this.x = x;
        this.y = y;
        this.z = z;
        this.edge = false;
    }

}

[Serializable]
public class AgentsData
{
    public List<AgentData> agents;

    public AgentsData() => this.agents = new List<AgentData>();
}

public class Controller : MonoBehaviour
{
    private readonly string url = "http://localhost:8585";
    
    AgentsData agentsData;
    Dictionary<int, Vector3> carsPrevPositions, carsCurrPositions;
    Dictionary<int, GameObject> agentsCar;
    //Dictionary<int, GameObject> agentsStoplight;

    // GameObjects
    public GameObject[] cars;
    public GameObject[] stoplights;

    // Starting positions
    // UP, DOWN, LEFT, RIGHT
    readonly float[,] startingPos = new float[,] {{4.0f, 0.0f, 0.0f},
        {5.0f, 0.0f, -9.0f} , {0.0f, 0.0f, -5.0f}, {9.0f, 0.0f, -4.0f}};

    // Ending positions
    // DOWN, UP, RIGHT, LEFT
    readonly float[,] endingPos = new float[,] {{4.0f, 0.0f, -9.0f},
        {5.0f, 0.0f, 0.0f}, {9.0f, 0.0f, -5.0f}, {0.0f, 0.0f, -4.0f}};

    // Updates 
    public float timeToUpdate = 5.0f;
    private float timer, dt;
    private bool updated = false;


    // Start is called before the first frame update
    void Start()
    {
        agentsData = new AgentsData();

        agentsCar = new Dictionary<int, GameObject>();
        //agentsStoplight = new Dictionary<int, GameObject>();

        carsCurrPositions = new Dictionary<int, Vector3>();
        carsPrevPositions = new Dictionary<int, Vector3>();

        timer = timeToUpdate;

        StartCoroutine(StartSimulation());
    }

    IEnumerator StartSimulation()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{url}/init"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                //Debug.Log($" Starting simulation with: {webRequest.downloadHandler.text}");
                agentsData = JsonUtility.FromJson<AgentsData>(webRequest.downloadHandler.text);

                foreach (AgentData agent in agentsData.agents)
                {
                    Vector3 newAgentPosition = new Vector3(agent.x, agent.y, agent.z);

                    int carChosen = UnityEngine.Random.Range(0, 4);

                    carsPrevPositions[agent.id] = newAgentPosition;
                    agentsCar[agent.id] = Instantiate(cars[carChosen], newAgentPosition, Quaternion.identity);
                    agentsCar[agent.id].SetActive(false); // Hide the cars
                    
                }

                // Now lets get the stoplights
                for (int i = 0; i < stoplights.Length; i++)
                { // We add our stoplights based on the index of our stoplight array
                    float x = stoplights[i].transform.position.x;
                    float y = stoplights[i].transform.position.y;
                    float z = stoplights[i].transform.position.z;
                    AgentData newStoplight = new AgentData(i + 1, 2, x, y, z);
                    // We add the stoplight to the agentsData list
                    agentsData.agents.Add(newStoplight);
                }

                //for (int i = 0; i < agentsData.agents.Count; i++)
                //{
                //    Debug.Log(agentsData.agents[i].id);
                //}

                updated = true;
            } 
        }
    }

    IEnumerator GetAgentsData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
                Debug.Log(webRequest.error);
            else
            {
                //Debug.Log(webRequest.downloadHandler.text);
                agentsData = JsonUtility.FromJson<AgentsData>(webRequest.downloadHandler.text);

                //Debug.Log("-------------------------------ITERATION START-------------------------------");
                foreach (AgentData agent in agentsData.agents)
                {
                    //Debug.Log($"{agent.type}, {agent.id}, {agent.x}, {agent.y}, {agent.z}");

                    if (agent.type == 4) // It's a car
                    {
                        agent.edge = false;
                        // We check if the car is in a starting position
                        for (int pos = 0; pos < startingPos.GetLength(0); pos++)
                        {
                            if (startingPos[pos, 0] == agent.x && startingPos[pos, 1] == agent.y &&
                                startingPos[pos, 2] == agent.z)
                            { // The car is in a staring position

                                //Debug.Log($"Car {agentsCar[agent.id]} Starting Position: {startingPos[pos, 0]}, {startingPos[pos, 1]}, {startingPos[pos, 2]}");
                                //Debug.Log($"Car {agentsCar[agent.id]} Ending Position: {endingPos[pos, 0]}, {endingPos[pos, 1]}, {endingPos[pos, 2]}");

                                // We set its initial position
                                Vector3 startingDirection = new Vector3(startingPos[pos, 0], startingPos[pos, 1], startingPos[pos, 2]);

                                // We set the initial look rotation of the car
                                Vector3 carLookDirection = new Vector3(endingPos[pos, 0], endingPos[pos, 1], endingPos[pos, 2]);
                                Quaternion carRotation = Quaternion.LookRotation((carLookDirection-startingDirection), Vector3.up);

                                agentsCar[agent.id].transform.rotation = carRotation;
                                agentsCar[agent.id].transform.position = startingDirection;
                                carsCurrPositions[agent.id] = startingDirection;
                                carsPrevPositions[agent.id] = startingDirection;
                                agentsCar[agent.id].SetActive(true); // Show the car
                                agent.edge = true;
                                break;
                            }
                        }

                        // We check if the car is in a ending position
                        for (int pos = 0; pos < endingPos.GetLength(0); pos++)
                        {
                            if (endingPos[pos, 0] == agent.x && endingPos[pos, 1] == agent.y &&
                                endingPos[pos, 2] == agent.z)
                            { // The car is in a ending position

                                agentsCar[agent.id].SetActive(false);
                                Quaternion resetRotation = new Quaternion(0, 0, 0, 0);
                                Vector3 resetPosition = new Vector3(0, 0, 0);
                                agentsCar[agent.id].transform.rotation = resetRotation;
                                agentsCar[agent.id].transform.position = resetPosition;
                                agent.edge = true;
                                break;
                            }
                        }

                        // Move car :)
                        if (!agent.edge)
                        {
                            Vector3 newCarPositon = new Vector3(agent.x, agent.y, agent.z);
                            Vector3 currCarPos = new Vector3();
                            if (carsCurrPositions.TryGetValue(agent.id, out currCarPos))
                                carsPrevPositions[agent.id] = currCarPos;
                            carsCurrPositions[agent.id] = newCarPositon;
                        }
                    }
                    else // It's a stoplight 
                    {
                        
                        Light[] curr = stoplights[agent.id-1].GetComponentsInChildren<Light>();
                        if (agent.type == 1)
                        {
                            curr[0].intensity = 10.0f;
                            curr[1].intensity = 0f;
                            curr[2].intensity = 0f;
                            curr[3].color = Color.green;
                            //Debug.Log("WHAT: " + agent.id + " " + agent.type + " Deberia de ser verde");
                        }
                        else if (agent.type == 2)
                        {
                            curr[0].intensity = 0f;
                            curr[1].intensity = 10f;
                            curr[2].intensity = 0f;
                            curr[3].color = Color.yellow;
                            //Debug.Log("WHAT: " + agent.id + " " + agent.type + " Deberia de ser amarillo");
                        }
                        else if (agent.type == 3)
                        {
                            curr[0].intensity = 0f;
                            curr[1].intensity = 0f;
                            curr[2].intensity = 10.0f;
                            curr[3].color = Color.red;
                            //Debug.Log("WHAT: " + agent.id + " " + agent.type + " Deberia de ser rojo");
                        }
                    }

                }
                //Debug.Log("-------------------------------ITERATION END-------------------------------");

                updated = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"TIMER: {timer}");
        if (updated)
        {
            timer -= Time.deltaTime;
            dt = 1.0f - (timer / timeToUpdate);
        }

        if (timer < 0)
        {
            //Debug.Log("Getting Data");
            timer = timeToUpdate;
            updated = false;
            StartCoroutine(GetAgentsData());
        }

        foreach (var car in carsCurrPositions)
        {
            Vector3 currentPosition = car.Value;
            Vector3 previousPosition = carsPrevPositions[car.Key];

            Vector3 interpolated = Vector3.Lerp(previousPosition, currentPosition, dt);
            Vector3 direction = currentPosition - previousPosition;

            agentsCar[car.Key].transform.localPosition = interpolated;
            if (direction != new Vector3(0, 0, 0))
            {
                agentsCar[car.Key].transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
