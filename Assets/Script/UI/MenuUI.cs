using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public static MenuUI instance;
    [SerializeField] GameObject menuPanel;
    void Awake()
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

    void OnEnable()
    {
        UIManager.OnOpenMenu += OpenMenu;
        UIManager.OnCloseMenu += CloseMenu;

    }
    void OnDisable()
    {
        UIManager.OnOpenMenu -= OpenMenu;
        UIManager.OnCloseMenu -= CloseMenu;

    }
    public void OpenMenu()
    {

        menuPanel.SetActive(true);

    }
    public void CloseMenu()
    {

        menuPanel.SetActive(false);

    }
    public void BackToMain()
    {
        GameManager.instance.LoadScene("Start");
    }
    public void ToggleMenu()
    {
        if (!menuPanel.activeSelf)
        {
            UIManager.instance.OpenMenu();
        }
        else
        {
            UIManager.instance.CloseMenu();
        }
    }
}
