using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : InteractuableItems
{

    [SerializeField] private ParticleSystem water;
    [SerializeField] private GameObject seed;
    [SerializeField] private Transform actionPoint;
    [SerializeField] private float cooldownTime;
    private IEnumerator wateringCoroutine;

    void Start()
    {
        var emission = water.emission;
        emission.enabled = false;
    }

    void Update()
    {

    }

    private IEnumerator Watering(float cooldown)
    {
        while(true) 
        {
            float waitTime = Random.Range(0.1f, cooldown);
            yield return new WaitForSeconds(waitTime);
            GameObject seeds = GameObject.Instantiate(seed);
            seeds.transform.position = actionPoint.position;
            seeds.GetComponent<Rigidbody>().AddForce(Vector3.up);
        }
    }

    public override void Action() 
    {
        var emission = water.emission;
        emission.enabled = true;
        
        wateringCoroutine = Watering(cooldownTime);
        StartCoroutine(wateringCoroutine);
    }

    public override void StopAction() 
    {
        var emission = water.emission;
        emission.enabled = false;

        StopCoroutine(wateringCoroutine);
    }
}
