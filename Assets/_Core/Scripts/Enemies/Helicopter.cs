using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Transform rudder;
    public LayerMask playerLayer;
    Transform player;
    public float rutterSpeed = 1000;
    public float movementSpeed;
    public float rotatationSpeed;
    public float upForce = .1f;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
        transform.position += Vector3.up * .5f;
        GetComponent<DestroyOnCollision>().destroyOnCollision = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rudder.Rotate(Vector3.up * rutterSpeed * Time.fixedDeltaTime);
        var rb = GetComponent<Rigidbody>();

        Vector3 forward = player.position - transform.position;
        Quaternion targetRot = Quaternion.LookRotation(forward.normalized, Vector3.up);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotatationSpeed * Time.fixedDeltaTime));

        Vector3 targetPoint = player.TransformPoint(Vector3.forward * .5f);
        rb.MovePosition(Vector3.Lerp(rb.position, targetPoint, movementSpeed * Time.fixedDeltaTime));
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody != null && playerLayer.Contains(other.rigidbody.gameObject.layer))
        {
            GetComponent<DestroyOnCollision>().destroyOnCollision = true;
        }
    }
}
