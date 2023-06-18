using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float maxSpeed = 40f;
    public float jumpForce = 90f;
    public float bounceForce = 60f;
    private float timeSinceLastSpriteChange = 0f;
    public float spriteChangeInterval = 0.4f;
    private bool isGrounded = false;
    public bool menu_Begin = false;
    public bool menu_Quit = false;
    public bool sand_Splash_C = false;
    public bool light_Overlay1_C = false;
    private bool usingSprite1 = true;
    public Sprite Run1; // Running Sprite 1
    public Sprite Run2; // Running Sprite
    public Sprite Default_Idle; // Default Sprite
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        /* Set the player's spawn origin */
        transform.position = new Vector3(-65.5999985f, 34.4799995f, -117.510002f);
        moveSpeed = 100f;
        maxSpeed = 40f;
        jumpForce = 90f;
        bounceForce = 60f;
        spriteRenderer.sprite = Default_Idle;
    }

    void Update()
    {
        //Update Variables
        GameObject sandSplashObject = GameObject.FindWithTag("Sand_Splash");
        GameObject lightOverlay1 = GameObject.FindWithTag("Light-Overlay1");

        //Movement And Controls
        /* Horizontal Movement */
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.AddForce(moveDirection * moveSpeed);
        /* Clamps Character Velocity */
        if (rb.velocity.magnitude > maxSpeed)  
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        // Sprint
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("Sprinting!");
            jumpForce = 110f;
            Debug.Log("Velocity before: " + rb.velocity +" Max speed: " + maxSpeed);
            maxSpeed = 55f;
            spriteChangeInterval = 0.2f;
            var speedFactor = (maxSpeed - rb.velocity.magnitude) / maxSpeed;
            rb.AddForce(moveDirection * speedFactor * 5 * Vector2.right, ForceMode2D.Impulse);
            Debug.Log("Velocity after: " + rb.velocity + " Max speed: " + maxSpeed);
        }
        else 
        {
            maxSpeed = 40f;
            jumpForce = 90f;
            spriteChangeInterval = 0.4f;
        }




        /* Menu */
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = new Vector3(-66, 34, -117);
            isGrounded = false;
            sand_Splash_C = false;
            light_Overlay1_C = false;
            Debug.Log("Teleporting to Menu");
        }
        /* Checks if Character Grounded and Jumps */
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }


        /* Flip Character And Start/End Frame Change */
        if (Input.GetKey(KeyCode.A)){
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.D)){
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            timeSinceLastSpriteChange += Time.deltaTime;
            if (timeSinceLastSpriteChange >= spriteChangeInterval)
            {
                if (usingSprite1)
                {
                    spriteRenderer.sprite = Run1;
                }
                else
                {
                    spriteRenderer.sprite = Run2;
                }
                usingSprite1 = !usingSprite1;
                timeSinceLastSpriteChange = 0f;
            }
        }
        else{
            timeSinceLastSpriteChange = 0f;
            spriteRenderer.sprite = Default_Idle;
        }


        // Makes Splash Levels Appear
        if ( sand_Splash_C == true){
            sandSplashObject.transform.localScale = new Vector3(4, 3, 1);
        }
        else{
            sandSplashObject.transform.localScale = new Vector3(0, 0, 0);
        }
        /* Makes Light Overlay 1 Appear */
        if ( light_Overlay1_C == true){
            lightOverlay1.transform.localScale = new Vector3(120,110, 100);
        }
        else{
            lightOverlay1.transform.localScale = new Vector3(0, 0, 0);
        }

        // Executes Beginning
        if(Input.GetKeyDown(KeyCode.E) & menu_Begin == true){
            transform.position = new Vector3(-34, -107, 3);
            Debug.Log("Teleporting to LV. 1");
            sand_Splash_C = true;
            light_Overlay1_C = true;
        }

        //Exits Game
        if(Input.GetKeyDown(KeyCode.E) & menu_Quit == true){
            Debug.Log("Quitting Game");
            Application.Quit();
        }

       // Snap the position to the nearest pixel
       transform.position = SnapToPixel(transform.position);
    }

    // A method that snaps a vector to the nearest pixel
    private Vector2 SnapToPixel(Vector2 position)
    {
       // Get the pixels per unit of the sprite renderer
       SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
       float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;

       // Round the position to the nearest pixel
       float x = Mathf.Round(position.x * pixelsPerUnit) / pixelsPerUnit;
       float y = Mathf.Round(position.y * pixelsPerUnit) / pixelsPerUnit;

       // Return the snapped position
       return new Vector2(x,y);
    }

    /* CHECKS ALL TRIGGERS AND COLLISIONS */
    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Ground"))
      {
          isGrounded = true;
      }

    if (collision.gameObject.CompareTag("Bounce-Off"))
    {
        Debug.Log("Bouncing...");
        // Calculate the bounce direction
        Vector2 bounceDirection = transform.position - collision.transform.position;
        bounceDirection = bounceDirection.normalized;
        // Apply the bounce force
        rb.AddForce(bounceDirection * bounceForce + Vector2.down * bounceForce, ForceMode2D.Impulse);
        Debug.Log("Bounced!");
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
        if (other.tag == "Begin-Menu")
        {
            menu_Begin = false;
            Debug.Log(menu_Begin);
        }
        //Quit
        if (other.tag == "Quit_Menu")
        {
            menu_Quit = false;
            Debug.Log(menu_Quit);
        }
    }
}