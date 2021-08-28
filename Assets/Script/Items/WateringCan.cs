using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : InteractuableItems
{
  

    [SerializeField] int pourThreshold = 45;
    //[SerializeField] private ParticleSystem water;
    [SerializeField] private GameObject streamPrefab = null;
    [SerializeField] private Transform origin;
    [SerializeField] private float cooldownTime;
    private IEnumerator wateringCoroutine;
    [SerializeField] private bool isActive = false;
    private bool isPouring = false;

    private Stream currentStream = null;

    void Start()
    {
        //var emission = water.emission;
        //emission.enabled = false;
    }

    void Update()
    {
        
        if(isActive)
        {
            bool pourCheck = CalculatePourAngle() < pourThreshold;
            Debug.Log("pourCheck:" + pourCheck);
            Debug.Log("isPouring:" + isPouring);
            Debug.Log("angulo:" + CalculatePourAngle());
            if(pourCheck && !isPouring) 
            {
                isPouring = true;
                StartPour();
            }
            else if(!pourCheck)
            {
                EndPour();
            } 
        }
    
    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    { 
        if(currentStream != null) {
            currentStream.End();
            currentStream = null;
            isPouring = false;
        }
    }

    private float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

    /* private IEnumerator Watering(float cooldown)
    {
        while(true) 
        {
            float waitTime = Random.Range(0.1f, cooldown);
            yield return new WaitForSeconds(waitTime);
            GameObject seeds = GameObject.Instantiate(wat);
            seeds.transform.position = actionPoint.position;
            seeds.GetComponent<Rigidbody>().AddForce(Vector3.up);
        }
    } */

    public override void Action() 
    {
        isActive = true;
        //var emission = water.emission;
        //emission.enabled = true;
        
        //wateringCoroutine = Watering(cooldownTime);
        //StartCoroutine(wateringCoroutine);
    }

    public override void StopAction() 
    {
        isActive = false;

        EndPour();
        //var emission = water.emission;
        //emission.enabled = false;

        //StopCoroutine(wateringCoroutine);
    }
}
