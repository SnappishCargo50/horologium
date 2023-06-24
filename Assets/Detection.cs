using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

void OnCollisionEnter(Collision collision)
{
    //Enemy
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Debug.Log("Enemy Collided");
        
    }
    //Friendly
    if (collision.gameObject.CompareTag("Vendor1"))
    {
        Debug.Log("Vendor1 Collided");
    }
        if (collision.gameObject.CompareTag("Restart"))
    {
        Debug.Log("Teleporting...");
        transform.position = new Vector3(-35, 4, 3);
    }
}

void OnCollisionExit(Collision collision)
{
    //Enemy
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Debug.Log("Enemy Un-Collided");
    }
    //Friendly
        if (collision.gameObject.CompareTag("Vendor1"))
    {
        Debug.Log("Vendor Un-collided");
    }
        if (collision.gameObject.CompareTag("Restart"))
    {
        Debug.Log("Teleported");
    }
}
}
