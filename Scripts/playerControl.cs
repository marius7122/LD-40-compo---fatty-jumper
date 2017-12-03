using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

    private movementControl moveCnt;
    public float growAmount = 0.5f;
    private Rigidbody rb;

    //if player is fat balance > 0
    public int balance;
    //balance of player if eat all the apple from scene
    public int bestBallance;

    void Start()
    {
        moveCnt = gameObject.GetComponent<movementControl>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void grow()
    {
        //Vector3 velocity = rb.velocity * 1.5f;
        gameObject.transform.localScale += new Vector3(growAmount, growAmount, growAmount);
        //rb.velocity = velocity;

        //after getting fat you can jump less
        moveCnt.jumpAmount -= moveCnt.jumpAmount * 0.1f;
        //also the gravity will efect you more
        moveCnt.gravityScale += moveCnt.gravityScale * 0.1f;

        balance++;
        bestBallance++;
    }

    public void shrink()
    {
        //Vector3 velocity = rb.velocity * 1.5f;
        gameObject.transform.localScale -= new Vector3(growAmount, growAmount, growAmount);
        //rb.velocity = velocity;

        moveCnt.jumpAmount += moveCnt.jumpAmount * 0.1f;
        moveCnt.gravityScale -= moveCnt.gravityScale * 0.1f;

        balance--;
    }

}
