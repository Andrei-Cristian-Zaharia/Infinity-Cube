using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HideScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool change;
    public float waitTime;
    public bool isHide = false;

    private void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        Color c = sprite.material.color;

        if (!isHide)
            c.a = 255f;
        else c.a = 0f;

        sprite.material.color = c;
    }

    private void Update()
    {
        if (!change)
        {
            if (isHide)
                StartCoroutine(FadeIn());
            else StartCoroutine(FadeOut());

            change = true;
        }
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(waitTime);

        isHide = !isHide;
        change = false;
    }

    IEnumerator FadeIn()
    {
        for (float f = 0; f <= 1; f += 0.05f)
        {
            Color d = sprite.material.color;
            d.a = f;
            sprite.material.color = d;

            if (f > 0.2)
                if (this.GetComponent<BoxCollider2D>())
                    this.GetComponent<BoxCollider2D>().enabled = true;

            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(Change());
    }

    IEnumerator FadeOut()
    {
        for (float f = 1; f >= -0.05f; f -= 0.05f)
        {
            Color d = sprite.material.color;
            d.a = f;
            sprite.material.color = d;

            if (f < 0.1)
                if (this.GetComponent<BoxCollider2D>())
                    this.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(Change());
    }
}
