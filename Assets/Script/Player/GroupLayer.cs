using UnityEngine;

public class GroupLayer : MonoBehaviour
{
    SpriteRenderer parentSpriteRenderer;
    SpriteRenderer[] chillSpriteRenderers;
    Canvas[] canvas;

    void Awake()
    {
        parentSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        chillSpriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        canvas = gameObject.GetComponentsInChildren<Canvas>();

    }

    void Start()
    {
        if (parentSpriteRenderer != null && chillSpriteRenderers != null && chillSpriteRenderers.Length > 0)
        {
            foreach (SpriteRenderer spriteRenderer in chillSpriteRenderers)
            {
                spriteRenderer.gameObject.layer = this.gameObject.layer;
                spriteRenderer.sortingLayerName = this.parentSpriteRenderer.sortingLayerName;
                spriteRenderer.sortingOrder = this.parentSpriteRenderer.sortingOrder;
            }
        }

        if (canvas != null)
        {
            foreach (Canvas canvasRenderer in canvas)
            {
                canvasRenderer.gameObject.layer = this.gameObject.layer;
                canvasRenderer.sortingLayerName = this.parentSpriteRenderer.sortingLayerName;
                canvasRenderer.sortingOrder = this.parentSpriteRenderer.sortingOrder;
            }
        }

    }
}
