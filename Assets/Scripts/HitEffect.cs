using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    float avalue;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        avalue = spriteRenderer.color.a;

        if(avalue >= 10)
        {
            Color tempColor = spriteRenderer.color;
            tempColor.a -= Time.deltaTime*0.5f;
            spriteRenderer.color = tempColor;
        }
        else
        {
            Destroy(this.gameObject);
        }
            
    }
}
