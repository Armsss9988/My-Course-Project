using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float fps;
    public TMP_Text fpstText;
    void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }
    void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpstText.text = fps.ToString();
    }
}
