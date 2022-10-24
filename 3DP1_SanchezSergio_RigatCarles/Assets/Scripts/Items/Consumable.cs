using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] ConsumableAsset consumableAsset;
    [SerializeField] Animator animator;
    [SerializeField] float consumeTime = 0.5f;

    public void consume(GameObject consumer)
    {
        if (consumableAsset.consume(consumer))
        {
            animator.SetTrigger("consume");
            StartCoroutine(destroyAfterAnimation());
        }
    }

    IEnumerator destroyAfterAnimation()
    {
        yield return new WaitForSeconds(consumeTime);
        Destroy(gameObject);
    }
}
