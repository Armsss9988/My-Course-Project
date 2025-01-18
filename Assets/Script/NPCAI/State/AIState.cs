using UnityEngine;

public abstract class AIState : ScriptableObject, IAIState
{
    public void CheckFlipx(AIController aiController)
    {
        float targetX = aiController.MovingTarget.x;
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = aiController.GetCachedComponent<SpriteRenderer>();
        if ((targetX - rb2d.position.x) > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    public abstract void EnterState(AIController aiController);
    public abstract void ExitState(AIController aiController);

    public abstract void UpdateState(AIController aiController);
    public abstract void FixedUpdateState(AIController aiController);

}