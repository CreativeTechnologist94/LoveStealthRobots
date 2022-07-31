using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out EnemeyController _enemy))
        {
            _enemy.SetStunned();
            Destroy(gameObject);
        }
    }
}
