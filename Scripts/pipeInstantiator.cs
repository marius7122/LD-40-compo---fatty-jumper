using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeInstantiator : MonoBehaviour {

    public playerControl pc;
    public GameObject pipePrefab;
    public GameObject applePrefab;
    
    //time between 2 instantiations
    public float spawnTime = 1f;
    private float lastInstantce;

    private float lowestGapPosition = -2.5f;
    private float highestGapPosition = 2.5f;

    private float lowestApplePosition = -2f;
    private float highestApplePosition = 2f;

    private float minAppleTime = 5f;
    private float maxAppleTime = 15f;
    private int chanceOfApple = 2;
    private float lastApple;

    public float speed = 10f;

    private bool gameStarted;
    public bool gameEnded;

    void Start()
    {
        lastInstantce = Time.time;
    }

    void Update()
    {
        //first space press
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
            gameStarted = true;

        if (gameStarted && !gameEnded && Time.time > lastInstantce + spawnTime)
        {
            if (pc.bestBallance > 0 && Random.Range(0, chanceOfApple) == 0)
            {
                createNewPipesWithApple(speed, Random.Range(lowestGapPosition, highestGapPosition), Random.Range(lowestApplePosition, highestApplePosition));
                pc.bestBallance--;
            }
            else
                createNewPipes(speed, Random.Range(lowestGapPosition, highestGapPosition));
            lastInstantce = Time.time;
        }
    }

    void createNewPipes(float speed, float gapPosition)
    {
        GameObject pipe = Instantiate(pipePrefab) as GameObject;
        pipe.GetComponent<pipes>().setPipes(speed, gapPosition);
    }

    void createNewPipesWithApple(float speed,float gapPosition, float appleYPosition)
    {
        Debug.Log("attention, an apple is comming!");

        GameObject pipe = Instantiate(pipePrefab) as GameObject;
        pipe.GetComponent<pipes>().setPipes(speed, gapPosition);

        Vector3 applePosition = new Vector3(10f + speed * spawnTime * 0.5f, appleYPosition, 0f);

        GameObject apple = Instantiate(applePrefab, applePosition, Quaternion.identity) as GameObject;
        apple.transform.parent = pipe.transform;

        pc.bestBallance--;
    }

}
