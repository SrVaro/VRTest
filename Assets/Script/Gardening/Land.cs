using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{

    [SerializeField] private Mesh plowedLandMesh;
    [SerializeField] private Mesh uncultivatedLandMesh;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = uncultivatedLandMesh;
        gameObject.layer = LayerMask.NameToLayer("SownLand");
        
    }

    public void Plow() {
        GetComponent<MeshFilter>().mesh = plowedLandMesh;
        gameObject.layer = LayerMask.NameToLayer("PlowedLand");
    }

}
