using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTrigger : MonoBehaviour
{

    bool isGrabing = false;
    Rigidbody rigidbody;
    FixedJoint currentConnectingObj = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGrabing)
            return;
        if (other.CompareTag("Grabable"))
        {
            GrabObject(other.GetComponent<Rigidbody>());
        }
    }

    private void Update()
    {
        if (currentConnectingObj && Input.GetButtonDown("Fire1"))
        {
            currentConnectingObj.GetComponent<Rigidbody>().AddForce(currentConnectingObj.transform.forward * 30);
            Destroy(currentConnectingObj);
        }
    }

    void GrabObject(Rigidbody obj)
    {
        
        FixedJoint fixedJoint = obj.GetComponent<FixedJoint>();
        if (fixedJoint == null)
            fixedJoint = obj.gameObject.AddComponent<FixedJoint>();
        
        fixedJoint.connectedBody = rigidbody;
        fixedJoint.breakForce = 6000;
        currentConnectingObj = fixedJoint;
    }
}
