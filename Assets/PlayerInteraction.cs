using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //   Rigidbody2d rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //          string layer = LayerMask.LayerToName(this.layer);
            //           RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask(layer));
            //           if (hit.collider != null)
            //           {
            //               Item npc = hit.collider.GetComponent<Collectable>();
            //               if (npc != null)
            //                {
            //               }
            //
            //          }
        }
    }
}
