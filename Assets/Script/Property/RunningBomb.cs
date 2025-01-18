using UnityEngine;

public class RunningBomb : MonoBehaviour
{
    Animator animator;
    Detector enemyRangeDetect;
    SpriteRenderer spriteRenderer;
    public GameObject explode;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRangeDetect = GetComponent<Detector>();
    }
    private void Update()
    {
        /* if (enemyRangeDetect.IsInDetectRange())
         {
             animator.SetBool("Detect", true);
         }
         else
         {
             animator.SetBool("Detect", false);
         }*/

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void DesTroyBullet()
    {
        GameObject effect = Instantiate(explode, this.transform.position, Quaternion.identity);
        effect.GetComponent<Effect>().SetSource(this.gameObject);
        effect.layer = this.gameObject.layer;
        effect.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = this.spriteRenderer.sortingLayerName;
        effect.gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.spriteRenderer.sortingOrder;
        Destroy(this.gameObject);
    }
}
