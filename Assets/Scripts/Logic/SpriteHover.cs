using UnityEngine;

public class SpriteHover : MonoBehaviour
{
    [SerializeField]
    Sprite hover;

    [SerializeField]
    Sprite normal;

    SpriteRenderer spriteRenderer;

    private const int kFramesCooldownForAudio = 8;
    private static int Frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normal = spriteRenderer.sprite;
        spriteRenderer.sprite = hover;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.sprite = normal;
        if (Time.frameCount > Frame + kFramesCooldownForAudio)
        {
            UtilSound.Instance.PlaySound("Hover", volume:0.45f);
            Frame = Time.frameCount;
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = hover;
    }
}
