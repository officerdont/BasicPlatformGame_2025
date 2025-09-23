using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject Platformprefab;
    private float inputHorizontal;
    private int maxNumJumps;
    private int numJumps;
    public int hammer = 0;
    //because this is public we have access to it in the unity editor
    public float horizontalMoveSpeed;
    public float jumpForce;

    public GameObject doubleJumpHatLocation;
    public GameObject HammerLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //I can only get this component becuase the rigidbody2d is attached to the player
        //this script is also attached to the player
        rb = GetComponent<Rigidbody2D>();

        maxNumJumps = 1;
        numJumps = 1;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        jump();
        platformcreator();
    }

    private void movePlayerLateral()
    {
        //if A/D/<-/-> are pressed move the player accordingly
        //"Horizontal" is defined in the input section of the project settings
        //the line below will return:
        //0 - no button pressed
        //1 - right arrow or d pressed
        //2 - left arrow or a pressed.
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        flipPlayerSprite(inputHorizontal);
        rb.linearVelocity = new Vector2(horizontalMoveSpeed * inputHorizontal, rb.linearVelocity.y);
    }

    private void flipPlayerSprite(float inputHorizontal)
    {
        //this is how we will make the player face the direction they are moving
        if (inputHorizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (inputHorizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && numJumps <= maxNumJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            numJumps++;
        }
    }


    //Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision will contain information about the object that the player collided with 
        //Debug.Log(collision.gameObject);
        if(collision.gameObject.CompareTag("Ground"))
        {
            numJumps = 1;
        }
        else if (collision.gameObject.CompareTag("obBottom"))
        {
            // die
            SceneManager.LoadScene("SampleScene");
        }
       

    }

    //Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //double jump
        if(collision.gameObject.CompareTag("DoubleJump"))
        {
            GameObject hat = collision.gameObject;
            equipDoubleJumpHat(hat);
            maxNumJumps = 2;
        }
        else if (collision.gameObject.CompareTag("Hammer"))
        {
            GameObject Hammer = collision.gameObject;
            equipHammer(Hammer);
            hammer++;
            

        }
    }

    private void equipDoubleJumpHat(GameObject hat)
    {
        hat.transform.position = doubleJumpHatLocation.transform.position;
        hat.gameObject.transform.SetParent(this.gameObject.transform);
    }

    private void equipHammer(GameObject Hammer)
    {
        Hammer.transform.position = HammerLocation.transform.position;
        Hammer.gameObject.transform.SetParent(this.gameObject.transform);
    }

    private void platformcreator()
    {

        if (Input.GetKeyDown(KeyCode.Q) && hammer > 0)
        {
            // spawn platform beneth the player
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y - 1.5f);
            Instantiate(Platformprefab, spawnPosition, Quaternion.identity);
           // hammer--;
           

        }
    }
}   

