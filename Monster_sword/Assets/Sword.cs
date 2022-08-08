using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool on = false;
    private MeshCollider col;
    private void Start()
    {
        col=transform.GetComponent<MeshCollider>();
    }
    private void OnTriggerStay(Collider target)
    {
        if (on)
        {
            if (target.CompareTag("enemy")) { 
                Debug.Log(target.gameObject.name);
                Destroy(target.gameObject);
            }

        }
        
    }
    public void changer(bool var)
    {
        on = var;
    }
}
