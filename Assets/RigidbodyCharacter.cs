using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{

    public float Speed = 10f;
    public float JumpHeight = 4f;
    public float GroundDistance = 0.0f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }
void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        _isGrounded = true;
    }
}

void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        _isGrounded = false;
    }
}

    void Update()
    {

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}

