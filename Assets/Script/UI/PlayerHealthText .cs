using TMPro;
using UnityEngine;

public class PlayerHealthText : MonoBehaviour
{
    TMP_Text healthText;
    public static PlayerHealthText instance { get; private set; }
    void Awake()
    {
        instance = this;
        healthText = GetComponent<TMP_Text>();
    }

    public void SetText(float min, float max)
    {
        healthText.text = Mathf.Floor(min) + "/" + Mathf.Floor(max);
    }
}
