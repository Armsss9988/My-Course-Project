using System;

[Serializable]
public class EquipmentData
{
    public string torso;
    public string pant;
    public string shoes;
    public string gloves;
    public string shield;
    public string arrow;

    public EquipmentData(string torso, string pant, string shoes, string gloves, string shield, string arrow)
    {
        this.torso = torso;
        this.pant = pant;
        this.shoes = shoes;
        this.gloves = gloves;
        this.shield = shield;
        this.arrow = arrow;
    }
}
