using UnityEngine;

public class Bridge : MonoBehaviour
{
    Transform cornerLeft, cornerRight, middle;
    string currentStringLayer, currentElevation;
    public LayerTriggerData layerTrigger;
    int currentLayer;


    // Start is called before the first frame update
    void Awake()
    {
        currentLayer = this.gameObject.layer;
        currentStringLayer = LayerMask.LayerToName(currentLayer);
        currentElevation = "Elevation " + currentStringLayer.Substring(currentStringLayer.Length - 1);
        middle = this.transform.Find("Middle");
        cornerLeft = this.transform.Find("Corner Left");
        cornerRight = this.transform.Find("Corner Right");
        ConfigGrandChild();
        cornerLeft.gameObject.layer = currentLayer;
        cornerRight.gameObject.layer = currentLayer;
        middle.gameObject.layer = LayerMask.NameToLayer("Bridge");
    }
    void ConfigGrandChild()
    {
        Transform[] allChildren = this.transform.GetComponentsInChildren<Transform>(true); // include inactive
        foreach (Transform child in allChildren)
        {
            if (child.parent != this.transform && child.name == "Collider In")
            {
                child.gameObject.layer = LayerMask.NameToLayer(currentElevation);
                child.GetComponent<LayerTrigger>().layer = layerTrigger.layerIn;
                child.GetComponent<LayerTrigger>().sortingLayer = layerTrigger.sortingLayerIn;

            }
            if (child.parent != this.transform && child.name == "Collider Out")
            {
                child.gameObject.layer = LayerMask.NameToLayer(currentElevation);
                child.GetComponent<LayerTrigger>().layer = layerTrigger.layerOut;
                child.GetComponent<LayerTrigger>().sortingLayer = layerTrigger.sortingLayerOut;
            }
        }

    }

}