using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class HittableBlockQuestion : MonoBehaviour
{
    [SerializeField] private UnityEvent _hit;
    public GameObject coin;
    private bool alreadyHit = false;
    private GameObject coinGameObject;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<PlayerMovement>();
        if (player && col.contacts[0].normal.y > 0 && !alreadyHit)
        {
            alreadyHit = true;
            _hit.Invoke();
            StartCoroutine(Animation());
            player.score += 100;
            player.coins++;
        }
    }

    public IEnumerator Animation()
    {
        coinGameObject =  Instantiate(coin, transform.position, Quaternion.identity);
        Vector3 restingPos = coinGameObject.transform.localPosition;
        Vector3 animatedPos = restingPos + Vector3.up * 0.2f;

        yield return Move(restingPos, animatedPos);
        yield return Move(animatedPos, restingPos);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        Vector3 to2 = new Vector3(to.x, to.y + 1f, to.z);
        float elapsed = 0f;
        float duration = 0.15f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;

            coinGameObject.transform.localPosition = Vector3.Lerp(from, to2, t);
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        coinGameObject.transform.localPosition = to;
    }
}
