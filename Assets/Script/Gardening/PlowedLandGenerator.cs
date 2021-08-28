using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlowedLandGenerator : MonoBehaviour
{
    [SerializeField] private GameObject plowedLandPrefab;
    [SerializeField] private int width;
    [SerializeField] private int length;
    // Start is called before the first frame update
    void Start()
    {
        for (float i = 0; i < width / 2; i += 0.5f)
        {
            GameObject.Instantiate(plowedLandPrefab, transform.position + new Vector3(i, 0 , 0), Quaternion.identity);
            for (float j = 0; j < length / 2; j += 0.5f)
            {
                GameObject.Instantiate(plowedLandPrefab, transform.position + new Vector3(i, 0 , j), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
