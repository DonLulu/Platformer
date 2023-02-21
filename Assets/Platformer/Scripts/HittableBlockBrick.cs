using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HittableBlockBrick : MonoBehaviour
{
    [SerializeField] private UnityEvent _hit;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<PlayerMovement>();
        if (player.isMarioBig && col.contacts[0].normal.y > 0)
        {
            _hit.Invoke();
            player.score += 100;
        }
    }
}
