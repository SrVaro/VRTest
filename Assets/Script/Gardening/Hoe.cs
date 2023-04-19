using UnityEngine;

public class Hoe : MonoBehaviour
{

    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("UnplowedLand")) collision.gameObject.GetComponent<Land>().Plow();
    }
}
