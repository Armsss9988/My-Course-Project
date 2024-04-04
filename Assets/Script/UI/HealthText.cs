using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    TMP_Text healthText;
    void Awake()
    {
        healthText = GetComponent<TMP_Text>();
    }

    public void SetText(float min, float max)
    {
        healthText.text = Mathf.Floor(min) + "/" + Mathf.Floor(max);
    }
}
