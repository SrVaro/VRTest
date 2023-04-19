using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private GameObject plant;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PlowedLand")) {
            collision.gameObject.layer = LayerMask.NameToLayer("SownLand");
            GameObject newTree = GameObject.Instantiate(plant, transform.position, Quaternion.identity, collision.transform);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
