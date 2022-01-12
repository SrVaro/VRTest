using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMenu : MonoBehaviour
{

    [SerializeField]
    private Transform spawnPoint;

    // Start is called before the first frame update
    public void SpawnObject(GameObject gm) {
        Instantiate(gm, spawnPoint.position, Quaternion.identity);
    }


}
