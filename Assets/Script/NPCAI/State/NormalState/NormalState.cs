using UnityEngine;
[CreateAssetMenu(fileName = "NormalState", menuName = "NormalState")]
public class NormalState : MoveState
{
    public override void EnterState(AIController aiController)
    {
        base.EnterState(aiController);
    }
    public override void ExitState(AIController aiController)
    {
        base.ExitState(aiController);

    }

    public override void UpdateState(AIController aiController)
    {

        base.UpdateState(aiController);
    }

    public override void FixedUpdateState(AIController aiController)
    {
        Debug.Log("Finding Moving Target");
        Rigidbody2D rb2d = aiController.GetCachedComponent<Rigidbody2D>();
        Vector2 firstPosition = aiController.FirstPosition;
        Debug.Log("Stop range: " + (rb2d.position - aiController.MovingTarget).magnitude + " and velo: " + rb2d.linearVelocity.magnitude);
        if ((rb2d.position - aiController.MovingTarget).magnitude <= 0.1f || rb2d.linearVelocity.magnitude == 0)
        {
            SetMovingTarget(aiController, new Vector2(Random.Range(firstPosition.x - 3, firstPosition.x + 3), Random.Range(firstPosition.y - 3, firstPosition.y + 3)));
        };
        base.FixedUpdateState(aiController);

    }
}
