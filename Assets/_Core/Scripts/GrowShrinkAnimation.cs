using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShrinkAnimation : MonoBehaviour
{
    Vector3 originalSize;
    Vector3 targetSize;
    public float animSpeed;
    private float timer;
    public bool growOnEnable = true;
    private void Awake()
    {
        originalSize = transform.localScale;
        targetSize = originalSize;
    }
    private void OnEnable()
    {
        if (growOnEnable)
        {
            Grow();
        }
    }
    public void Grow()
    {
        transform.localScale = Vector3.zero;
        targetSize = originalSize;
        timer = 0;
    }
    public void Shrink()
    {
        transform.localScale = originalSize;
        targetSize = Vector3.zero;
        timer = 0;
    }
    public void ShrinkAndDestroy(float length)
    {
        animSpeed = length;
        Shrink();
        Destroy(gameObject, length);
    }

    private void Update()
    {
        if (transform.localScale != targetSize)
        {
            if (animSpeed > 0)
            {
                timer += Time.deltaTime;
                float percentage = Mathf.Clamp01(timer / animSpeed);
                transform.localScale = Vector3.Lerp(transform.localScale, targetSize, percentage);
            }
            else
            {
                transform.localScale = targetSize;
            }

        }
    }
}
