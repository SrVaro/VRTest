using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private Vector3 treeScale;
 
    //private int growState = 0;

    [SerializeField] private GameObject plant;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PlowedLand")) {
            collision.gameObject.layer = LayerMask.NameToLayer("SownLand");
            GameObject newTree = GameObject.Instantiate(plant, transform.position, Quaternion.identity, collision.transform);
            Destroy(gameObject);
        }
    }
}
