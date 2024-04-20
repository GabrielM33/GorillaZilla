using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigFollower : MonoBehaviour
{
    public enum BodyPart{
        Head,
        LeftHand,
        RightHand
    }
    public BodyPart bodyPart;
    Transform followTarget;

    void Start()
    {
        OVRCameraRig rig = GameObject.FindObjectOfType<OVRCameraRig>();
        switch(bodyPart){
            case BodyPart.Head:
                followTarget = rig.centerEyeAnchor;
                break;
                case BodyPart.LeftHand:
                followTarget = rig.leftHandAnchor;
                break;
                case BodyPart.RightHand:
                followTarget = rig.rightHandAnchor;
                break;
        }  
    }

    void Update()
    {
        transform.position = followTarget.position;
        transform.rotation = followTarget.rotation;
    }
}
