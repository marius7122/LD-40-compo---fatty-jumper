using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class pipes : MonoBehaviour
{

    private Vector3 originalUpPipePosition = new Vector3(10f, 5f, 0f);
    private Vector3 originalDownPipePosition = new Vector3(10f, -5f, 0f);
    private Vector3 speedVector;

    Transform upPipe;
    Transform downPipe;

    public float gapSize = 3.5f;
    public float speed;

    public bool haveApple;
    public bool move = true;

    //initialization of this pipes
    public void setPipes(float speed, float gapPosition)
    {
        this.speed = speed;
        speedVector = new Vector3(speed, 0f, 0f);

        upPipe = gameObject.transform.GetChild(0);
        downPipe = gameObject.transform.GetChild(1);

        transform.position = new Vector3(0f, gapPosition, 0f);

        //if there is no pipe gap
        if (upPipe.transform.position == originalUpPipePosition)
        {
            changePipesGapSize(gapSize);
        }
    }

    void changePipesGapSize(float size)
    {
        Vector3 halfGap = new Vector3(0f, size / 2, 0f);

        upPipe.position = originalUpPipePosition + halfGap;
        downPipe.position = originalDownPipePosition - halfGap;
    }

    void Update()
    {
        if(move)
            updatePosition();

        //if isn't anymore in view range
        if (gameObject.transform.position.x <= -25)
        {
            //the player didn't eat the apple
            if (haveApple)
                GameObject.Find("Main Caracter").GetComponent<playerControl>().bestBallance--;

            Destroy(this.gameObject);
        }
    }

    void updatePosition()
    {
        gameObject.transform.position -= speedVector * Time.deltaTime;
    }
}
