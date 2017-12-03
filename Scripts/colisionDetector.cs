using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisionDetector : MonoBehaviour {

    public gameplayScript GS;
    public playerControl PC;
    public laserScript LS;

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;

        if (tag == "enemy")
            GS.endGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (tag == "coin")
        {
            GS.addCoin();
            //destroy the coin i hit
            Destroy(other.gameObject);
        }

        if(tag == "apple")
        {
            PC.shrink();
            //destroy the apple i hit
            Destroy(other.gameObject);
        }

        if (tag == "laser")
        {
            LS.playerWasHit();
        }
    }
}
