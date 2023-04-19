using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPiece : MonoBehaviour
{
    private int health = 100;
    [SerializeField] private Computer comp;

    [SerializeField] private Material[] listMaterial;
    private MeshRenderer mr;
    private Rigidbody rg;

    void Start() {
        mr = GetComponent<MeshRenderer>();
        rg = GetComponent<Rigidbody>();
    }

    private void GetDamage() {
        health -= 5;
        CheckStatus();
        Debug.Log("Health: " + health);
    }

    private void CheckStatus() {
        if(health >= 0 && health < 25){
            mr.material = listMaterial[3];
        } else if (health >= 25 && health < 50) {
            rg.isKinematic = false;
            rg.useGravity = true;
            transform.parent = null;

            mr.material = listMaterial[2];
        } else if (health >= 50 && health < 75) {
            mr.material = listMaterial[1];
        } else if (health >= 75 && health < 100) {
            mr.material = listMaterial[0];
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= 0.5f) {
            GetDamage();
        } 
    }

}
