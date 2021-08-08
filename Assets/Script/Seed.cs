using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private GameObject tree;
    private Vector3 treeScale;
    private float randRot;
    private float randScale;

    // Start is called before the first frame update
    void Start()
    {
        randRot = Random.Range(0f, 180f);
        randScale = Random.Range(0f, 0.002f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            GameObject newTree = GameObject.Instantiate(tree, transform.position, Quaternion.identity);
            newTree.transform.Rotate(Vector3.up, randRot);
            StartCoroutine("TreeSpawn", newTree);
        }
    }

    private IEnumerator TreeSpawn(GameObject newTree)
    {
        treeScale = Vector3.zero;
        newTree.transform.localScale = treeScale;
        

        float start = 0;
        while (start < 100) 
        {
            yield return new WaitForSeconds(0.01f);
            treeScale += new Vector3(randScale, randScale, randScale);
            newTree.transform.localScale = treeScale;
            start += 1;
        }
    }
}
