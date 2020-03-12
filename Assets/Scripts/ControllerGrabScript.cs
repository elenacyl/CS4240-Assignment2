using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabScript : MonoBehaviour
{

	public SteamVR_Input_Sources handType;
	public SteamVR_Behaviour_Pose controllerPose;
	public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean triggerAction;

    private float thrust = 25.0f;
	private GameObject collidingObject;
	private GameObject objectInHand;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetLastStateDown(handType) && objectInHand == null)
        {
            if (collidingObject)
            {
                GrabObject();
                source.Play();
            }
        }

        if (triggerAction.GetLastStateDown(handType))
        {
            if (objectInHand)
            {
                ShootBullet();
            } else {
                // mouse click here
            }
        }

        if (triggerAction.GetLastStateUp(handType)) {
            objectInHand = null;
        }

        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        };

        collidingObject = null;
    }

    void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        };

        collidingObject = col.gameObject;
    }

    void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody> ();
    }

    FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint> ();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    void ShootBullet()
    {
        // ReleaseObject();\
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
        }
        objectInHand = null;
    }

    void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        objectInHand = null;
    }
}
