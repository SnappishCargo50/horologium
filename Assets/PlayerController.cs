using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float maxSpeed = 60f;
    public float jumpForce = 80f;
    private bool isGrounded = false;
    public bool menu_Begin = false;
    public bool menu_Quit = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        /* Horizontal Movement */
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.AddForce(moveDirection * moveSpeed);
        /* Clamps Character Velocity */
        if (rb.velocity.magnitude > maxSpeed)  
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        /* Sprint */
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Debug.Log("Sprinting!");
            Debug.Log("Velocity before: " + rb.velocity);
            maxSpeed = 80f;
            rb.AddForce(new Vector2(20, 0));
            Debug.Log("Velocity after: " + rb.velocity);
        }
        else {
            maxSpeed = 60f;
        }
        /* Menu */
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = new Vector3(-66, 34, -117);
            isGrounded = false;
            Debug.Log("Teleporting to Menu");
        }
        /* Checks if Character Grounded and Jumps */
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
        /* Flip Character */
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // Excutes Begining
        if(Input.GetKeyDown(KeyCode.E) & menu_Begin == true){
            transform.position = new Vector3(-34, -107, 3);
            Debug.Log("Teleporting to LV. 1");
        }

        //Exits Game
        if(Input.GetKeyDown(KeyCode.E) & menu_Quit == true){
            Debug.Log("Quitting Game");
            Application.Quit();
        }
    }

        /* CHECKS ALL TRIGGERS AND COLLISIONS */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    /* Trigger Enter */
    void OnTriggerEnter2D(Collider2D other)
    {
        /* Main Menu Options */
        //Begin
        if (other.tag == "Begin-Menu")
        {
            menu_Begin = true;
            Debug.Log(menu_Begin);
        }
        //Quit
        if (other.tag == "Quit_Menu")
        {
            menu_Quit = true;
            Debug.Log(menu_Quit);
        }
    }
    /* Trigger Exit */
        void OnTriggerExit2D(Collider2D other)
    {
        /* Main Menu Options */
        //Begin
        if (other.tag =="Begin-Menu")
        {
            menu_Begin = false;
            Debug.Log(menu_Begin);
        }
    }

}