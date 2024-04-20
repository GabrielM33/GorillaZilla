using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBlast : MonoBehaviour
{
    public float speed;
    public float growSpeed;
    public float destroyTime = 10f;
    public Vector3 targetSize;
    Vector3 myForward;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
       myForward = transform.forward;
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos += myForward * Time.fixedDeltaTime * speed;
        transform.position = pos;
        transform.forward = myForward;
        var target = Vector3.Lerp(transform.localScale, targetSize, Time.deltaTime * growSpeed);
        transform.localScale = target;
    }

}
