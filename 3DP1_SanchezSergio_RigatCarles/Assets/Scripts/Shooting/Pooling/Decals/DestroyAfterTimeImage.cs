using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeImage : DestroyAfterTime
{
    [SerializeField] float fadeOutTime;
    float colourR, colourG, colourB, colourA;
    MeshRenderer meshRenderer;

    IEnumerator destroyCoroutine;

    void Awake()
    {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();

        colourR = meshRenderer.material.color.r;
        colourG = meshRenderer.material.color.g;
        colourB = meshRenderer.material.color.b;
        colourA = meshRenderer.material.color.a;
    }

    public override void OnEnable() 
    {
        if(destroyCoroutine != null) 
            StopCoroutine(destroyCoroutine);
        meshRenderer.material.color = new Color(colourR, colourG, colourB, colourA);
        destroyCoroutine = destroyAfterTime(time);
        StartCoroutine(destroyCoroutine);
    }

    IEnumerator destroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        float alpha = colourA;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime / fadeOutTime;
            meshRenderer.material.color = new Color(colourR, colourG, colourB, alpha);
            yield return new WaitForEndOfFrame();
        }
        gameObject.transform.parent = poolParent;
        gameObject.SetActive(false);
    }
}
