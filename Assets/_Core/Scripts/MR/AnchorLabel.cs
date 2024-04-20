using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var anchor = GetComponent<OVRSceneAnchor>();
        name = $"[{name}] - {anchor.Uuid}";
    }
}
