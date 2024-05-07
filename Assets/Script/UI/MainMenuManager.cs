using Unity.Services.Authentication;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    [SerializeField] GameObject menuPanel;


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

    public void ExitGame()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        GameManager.instance.StartNewGame();
    }
    public void SignOut()
    {
        try
        {
            AuthManager.instance.SignOut();
            GameManager.instance.LoadScene("Auth");
        }
        catch
        {

        }


    }
    public void IsLoggedIn()
    {

    }
    public void IsSignedInFailed()
    {

    }
    public void IsLoggedOut()
    {

    }

    private void OnEnable()
    {
        AuthenticationService.Instance.SignedIn += () => { IsLoggedIn(); };
        AuthenticationService.Instance.SignedOut += () => { IsLoggedOut(); };
        AuthenticationService.Instance.SignInFailed += (err) =>
        {
            IsSignedInFailed();
        };

    }
    private void OnDisable()
    {
        AuthenticationService.Instance.SignedOut -= () => { IsLoggedIn(); };
        AuthenticationService.Instance.SignedIn -= () => { IsLoggedOut(); };
        AuthenticationService.Instance.SignInFailed -= (err) =>
        {
            IsSignedInFailed();
        };
    }
}
