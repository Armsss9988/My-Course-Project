using UnityEngine;

[CreateAssetMenu(fileName = "Layer Trigger Data", menuName = "Layer Trigger Data", order = 49)]
public class LayerTriggerData : ScriptableObject
{
    public string layerIn;
    public string sortingLayerIn;
    public string layerOut;
    public string sortingLayerOut;
}
