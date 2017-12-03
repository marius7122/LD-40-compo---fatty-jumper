using UnityEngine;
using UnityEngine.UI;

public class gameplayScript : MonoBehaviour {


    public int coins;
    public Text coinsText;
    public GameObject coinsUI;
    public Text score;
    public GameObject gameOverPannel;
    public Text totalCoinsText;
    public Text highscoreText;
    public GameObject newHighscoreUI;

    public void endGame()
    {
        GameObject[] Pipes = GameObject.FindGameObjectsWithTag("pipe");

        foreach (GameObject g in Pipes)
        {
            g.GetComponent<pipes>().move = false;

            //make pipes trigger because i want the bird to fall on floor
            g.transform.GetChild(1).GetComponent<BoxCollider>().isTrigger = true;
        }

        //i stop pipe instantiantion
        GameObject.Find("Main Camera").GetComponent<pipeInstantiator>().gameEnded = true;

        GameObject player = GameObject.Find("Main Caracter");
        //I forbid the player to jump
        player.GetComponent<movementControl>().gameEnded = true;
        //i move player a little bit, because i don't want to see weird collision between him and lower pipe
        player.transform.position += Vector3.back;
        //i stop the player movement (in case is jumping)
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //stop the laser if exist
        GameObject.Find("Laser").GetComponent<laserScript>().gameEnded = true;

        StopAllCoroutines();

        coinsUI.SetActive(false);

        //update the score from gameover panel
        score.text = coinsText.text;

        gameOverPannel.SetActive(true);


        //add colected coins to PlayerPrefs
        int totalCoins = PlayerPrefs.GetInt("coins");
        totalCoins += coins;
        PlayerPrefs.SetInt("coins", totalCoins);
        totalCoinsText.text = "Total Coins:  " + totalCoins.ToString();

        //check highscore
        int highscore = PlayerPrefs.GetInt("highscore");
        if(coins > highscore)
        {
            highscore = coins;
            newHighscoreUI.SetActive(true);
        }
        highscoreText.text = "Highscore:  " + highscore.ToString();
        PlayerPrefs.SetInt("highscore", highscore);
    }

    public void restartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void addCoin()
    {
        coins++;
        coinsText.text = coins.ToString();
    }

    public void stopTime()
    {
        Time.timeScale = 0f;
    }

    public void startTime()
    {
        Time.timeScale = 1f;
    }

}
