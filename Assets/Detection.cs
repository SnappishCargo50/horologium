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
    if (collision.gameObject.CompareTag("Friend"))
    {
        Debug.Log("Friend Un-Collided");
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
        if (collision.gameObject.CompareTag("Friend"))
    {
        Debug.Log("Friend Un-Collided");
    }
}
}

