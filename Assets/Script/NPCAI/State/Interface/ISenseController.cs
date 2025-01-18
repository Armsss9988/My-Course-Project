using UnityEngine;

public interface ISenseController
{
    public void SetTracker(ISense sense);
    public void OnDetectedTarget(Transform target);
    public void OnUndetectedTarget();
    public void OnDisableSense();
    public void OnEnableSense();

}
