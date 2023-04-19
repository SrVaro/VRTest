using UnityEngine;

public class Land : MonoBehaviour
{

    [SerializeField] private Mesh plowedLandMesh;
    [SerializeField] private Mesh UnplowedLandMesh;


    private bool plowed = false;

    void Start() {
    }

    public void Plow() {
        plowed = true;
        GetComponent<MeshFilter>().mesh = plowedLandMesh;
        gameObject.layer = LayerMask.NameToLayer("PlowedLand");
    }

    public void Unplow() {
        plowed = false;
        GetComponent<MeshFilter>().mesh = UnplowedLandMesh;
        gameObject.layer = LayerMask.NameToLayer("UnplowedLand");
    }

}
