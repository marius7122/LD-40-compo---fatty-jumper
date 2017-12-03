using UnityEngine.UI;
using UnityEngine;

public class movementControl : MonoBehaviour {

    public GameObject startText;
    public GameObject coinsUI;
    public GameObject infoUI;

    public float size = 1f;
    public float jumpAmount = 3f;

    private bool gameStarted;
    public bool gameEnded;

    private float globalGravity = -9.8f;
    public float gravityScale = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        //i will use an user defined gravity
        rb.useGravity = false;
    }

    void Update()
    {
        //jump button was pressed
        if (!gameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            //the game is now starting
            if (!gameStarted)
            {
                gameStarted = true;
                rb.isKinematic = false;

                startText.SetActive(false);
                coinsUI.SetActive(true);

                infoUI.SetActive(false);
            }

            jump();
        }
        
    }

    void FixedUpdate()
    {
        //apply gravity force
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void jump()
    {
        rb.velocity = Vector3.up * jumpAmount;
    }

}
