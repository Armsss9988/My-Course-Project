using UnityEngine;

public class LoadingScreenUI : MonoBehaviour
{
    public static LoadingScreenUI instance;
    [SerializeField] GameObject loadingBG;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OpenLoading()
    {
        loadingBG.SetActive(true);
    }
    public void CloseLoading()
    {
        loadingBG.SetActive(false);
    }
}
