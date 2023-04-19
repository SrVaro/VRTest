using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private float randRot;
    private int growState = 0;
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private SpriteRenderer spriteRenderer1;
    [SerializeField] private SpriteRenderer spriteRenderer2;
    [SerializeField] private int maxGrow;
    [SerializeField] private int growRate = 5;

    private int growTick;

    private bool watered = false;

    void Start()
    {
        LightingManager.OnTick_10 += TimeManager_OnTick;

        randRot = Random.Range(0f, 180f);
        transform.Rotate(Vector3.up, randRot);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Water") && growState < maxGrow) {
            watered = true;
            //GetComponentInParent<Material>();
        }
    }

    private void TimeManager_OnTick(object sender, LightingManager.OnTickEventArgs e)
    {
        if(growState != maxGrow && watered) 
        {
            growTick++;
            if((growTick % growRate) == 0) 
            {
                growState++;
                spriteRenderer1.sprite = spriteList[growState];
                spriteRenderer2.sprite = spriteList[growState];
                // watered = false;
                // COmentado solo para hacer rapido las pruebas
            }
        }
    }

    /* 
    private IEnumerator TreeSpawn(GameObject newTree)
    {
        newTree.transform.Rotate(Vector3.up, randRot);
        treeScale = Vector3.zero;
        newTree.transform.localScale = treeScale;

        float start = 0;
        while (start < 100) 
        {
            treeScale += new Vector3(randScale, randScale, randScale);
            newTree.transform.localScale = treeScale;
            start += 1;
            yield return new WaitForSeconds(0.01f);
        }
    }  
    */

}
