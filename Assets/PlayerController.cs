using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 30f;
    public float maxSpeed = 50f;
    public float jumpForce = 60f;
    private bool isGrounded = false;
    public bool menu_Begin = false;
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
            moveSpeed = 30f;
            return;
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
            transform.position = new Vector3(-35, -120, 3);
            Debug.Log("Teleporting to LV. 1");
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