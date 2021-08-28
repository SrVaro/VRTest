using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : MonoBehaviour
//ScriptableObject
{
    public new string name;
    public string description;
    private float randRot;
    private float randScale;
    private Vector3 treeScale;
    public int growState = 1;
    [SerializeField] int maxGrow = 10;
    private float increment = 0;

    void Start()
    {      
        transform.localScale = Vector3.zero;
        transform.Rotate(Vector3.up, randRot);

        randRot = Random.Range(0f, 180f);
        randScale = Random.Range(0.05f, 0.1f);  
        increment = (randScale / maxGrow);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.layer);
        Debug.Log(LayerMask.NameToLayer("Water"));
        if(collider.gameObject.layer == LayerMask.NameToLayer("Water") && growState < maxGrow) {
            growState += 1;

            treeScale = new Vector3(growState * increment, growState * increment, growState * increment);
            Debug.Log(treeScale);
            transform.localScale = treeScale;
        }
    }

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

}
