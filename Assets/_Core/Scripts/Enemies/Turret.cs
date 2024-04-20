using System.Collections;
using System.Collections.Generic;
using GorillaZilla;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float lookSpeed = 1;
    public Transform turretHead;
    public float fireRate;
    public float fireTimer;
    public float bulletSpeed = 5f;
    public GameObject bulletPrefab;
    public LayerMask laserLayer;


    // Update is called once per frame
    private void Awake()
    {
        fireTimer = fireRate;
    }
    void FixedUpdate()
    {
        LookAtPlayer();
        if (Physics.Raycast(turretHead.position, turretHead.forward, out RaycastHit hitInfo, Mathf.Infinity, laserLayer, QueryTriggerInteraction.Collide))
        {
            if (hitInfo.collider.GetComponentInParent<Player>())
            {
                if (fireTimer >= fireRate)
                {
                    FireBullet();
                    fireTimer = 0;
                }
                fireTimer += Time.fixedDeltaTime;
            }

        }

    }
    void LookAtPlayer()
    {
        Transform playerHead = Camera.main.transform;
        Vector3 headPos = playerHead.position;
        Quaternion targetRotation = Quaternion.LookRotation(headPos - transform.position, Vector3.up);
        Quaternion curRotation = turretHead.rotation;
        //turretHead.LookAt(playerHead);
        //Quaternion targetRotation = turretHead.rotation;
        turretHead.rotation = Quaternion.Slerp(curRotation, targetRotation, Time.fixedDeltaTime * lookSpeed);
    }

    void FireBullet()
    {
        Vector3 spawnPoint = turretHead.position + turretHead.forward * .1f;
        var bulletGO = Instantiate(bulletPrefab, spawnPoint, turretHead.rotation, transform);
        bulletGO.GetComponent<Rigidbody>().AddForce(turretHead.forward * bulletSpeed);
        Destroy(bulletGO, 10f);
    }


}
