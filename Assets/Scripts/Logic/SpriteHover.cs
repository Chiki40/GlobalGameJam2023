using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHover : MonoBehaviour
{
    [SerializeField]
    Sprite hover;

    [SerializeField]
    Sprite normal;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normal = spriteRenderer.sprite;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.sprite = hover;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = normal;
    }
}
