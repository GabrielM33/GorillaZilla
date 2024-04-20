using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public bool destroyOnCollision = true;
    public GameObject explosionParticlePrefab;
    public LayerMask collideLayer;
    private void OnCollisionEnter(Collision other)
    {
        if (!destroyOnCollision) return;
        if (collideLayer.Contains(other.gameObject.layer))
        {
            BlowUp();
        }
    }
    
    public void BlowUp()
    {
        if(explosionParticlePrefab != null)
        {
            var particle = Instantiate(explosionParticlePrefab, transform.position, transform.rotation);
            particle.transform.localScale = Vector3.one * .05f;
        }
        
        Destroy(gameObject);
    }
}
