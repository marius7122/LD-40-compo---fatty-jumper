using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour {

    //public TrailRenderer trail;
    private float trailTime = 10f;
    public GameObject playerGO;
    private playerControl player;
    public GameObject warnSign;
    public GameObject trailMask;
    public GameObject trail;

    private Vector3 maskPosition = new Vector3(21.8f, 0f, 0f);
    private Vector3 trailPosition = new Vector3(8f, 0f, -2f);

    public float speed = 10f;
    private float minSpawnTime = 1f;
    private float maxSpawnTime = 3f;
    private float minYPos = -2.5f;
    private float maxYPos = 2.5f;
    public float warnTime = 1f;

    //x coordonate when laser run out of screen
    private float outOfScreenX = -11f;
    private float trailOffset = 0.0f;
    private float shootTime;

    private bool gameStarted;
    public bool gameEnded;
    private bool isShooting;
    private bool PlayerWasHit;

    void Start()
    {
        //trail = gameObject.GetComponent<TrailRenderer>();
        playerGO = GameObject.Find("Main Caracter");
        player = playerGO.GetComponent<playerControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            gameStarted = true;
            shootTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }

        if (gameStarted && !gameEnded && !isShooting && Time.time >= shootTime)
        {
            StartCoroutine(warnAndShoot());
        }
    }

    IEnumerator warnAndShoot()
    {
        isShooting = true;

        warnSign.SetActive(true);

        //exclamation mark is the first child
        GameObject exclamationMark = warnSign.transform.GetChild(0).gameObject;

        float warnStopTime = Time.time + warnTime;

        while(Time.time < warnStopTime)
        {
            //y position of warn sign must be the same as player
            exclamationMark.transform.position = new Vector3(exclamationMark.transform.position.x, playerGO.transform.position.y, 0f);

            yield return 0;
        }

        warnSign.SetActive(false);

        shoot();
    }

    void shoot()
    {
        float yPosition = playerGO.transform.position.y;

        Vector3 yPos = new Vector3(0f, yPosition, 0f);

        transform.position += yPos;
        trailMask.transform.position += yPos;

        yPos += Vector3.up * trailOffset;
        trail.transform.position += yPos;

        StartCoroutine(moveLaser());
    }

    void updatePosition()
    {
        Vector3 moveAmount = Time.deltaTime * speed * Vector3.left;
        transform.position = transform.position + moveAmount;
        trailMask.transform.position = trailMask.transform.position + moveAmount;
    }

    public void playerWasHit()
    {
        PlayerWasHit = true;

        player.grow();
    }

    IEnumerator moveLaser()
    {
        while(transform.position.x >= outOfScreenX && !PlayerWasHit)
        {
            updatePosition();
            yield return 0;
        }

        StartCoroutine(reinitializatePosition());
    }

    IEnumerator reinitializatePosition()
    {
        //trail.time = 0f;

        yield return 0;

        transform.position = new Vector3(11f, 0, 0f);
        //trail.time = trailTime;

        shootTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        isShooting = false;
        PlayerWasHit = false;


        trailMask.transform.position = maskPosition;
        trail.transform.position = trailPosition;
    }

}
