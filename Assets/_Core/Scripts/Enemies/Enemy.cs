using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEvent : UnityEvent<Enemy> { }
public class Enemy : MonoBehaviour
{
    public EnemyEvent onDestroy = new EnemyEvent();
    private void OnDestroy()
    {
        onDestroy?.Invoke(this);
    }
}
