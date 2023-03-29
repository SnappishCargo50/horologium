using System.Collections;
using System.Collections;
using UnityEngine;

public class Restart_Trigger : MonoBehaviour
{

public class Teleport : MonoBehaviour {

    public Vector3 teleportTarget;

    void OnTriggerEnter(Collider other) {
        other.transform.position = teleportTarget;
        Debug.Log("Teleported");
    }
}}

