using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSizer : InteractuableItems
{

    [SerializeField] private GameObject XROrigin;
    public override void Action() 
    {
        XROrigin.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
    }
}
