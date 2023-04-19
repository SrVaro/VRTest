using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : InteractuableItems
{
  

    [SerializeField] int pourThreshold = 45;
    [SerializeField] private GameObject streamPrefab = null;
    [SerializeField] private Transform origin;
    [SerializeField] private float cooldownTime;
    private IEnumerator wateringCoroutine;
    [SerializeField] private bool isActive = false;
    private bool isPouring = false;

    private Stream currentStream = null;


    void Update()
    {
        
        if(isActive)
        {
            
            bool pourCheck = CalculatePourAngle() < pourThreshold;
            //Debug.Log("pourCheck:" + pourCheck);
            //Debug.Log("isPouring:" + isPouring);
            //Debug.Log("angulo:" + CalculatePourAngle());

            // Se comprueba si la regadera esta en un angulo correcto y no esta activada
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

    public override void Action() 
    {
        isActive = true;
    }

    public override void StopAction() 
    {
        isActive = false;

        EndPour();
    }
}
