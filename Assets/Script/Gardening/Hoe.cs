using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : MonoBehaviour
{

    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collisionLayer:" + collision.gameObject.layer);

        if(collision.gameObject.layer == LayerMask.NameToLayer("SownLand")) {
            collision.gameObject.GetComponent<Land>().Plow();
        }
    }
}
