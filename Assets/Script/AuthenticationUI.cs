using System.Collections;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class AuthenticationUI : MonoBehaviour
{

    [SerializeField] TMP_InputField username;
    [SerializeField] TMP_InputField password;
    public GameObject notificationPanel;


    private async void Start()
    {

        await UnityServices.InitializeAsync();
        await AuthManager.instance.SignInCachedUserAsync();
        if (AuthenticationService.Instance.SessionTokenExists)
        {
            GameManager.instance.LoadScene("Start");
        }
        AuthenticationService.Instance.SignedIn += () => { IsLoggedIn(); };
        AuthenticationService.Instance.SignInFailed += (err) =>
        {
            ShowNotification(err.Message);
        };
        AuthManager.OnException += ShowNotification;
    }
    public async void SignUp()
    {
        await AuthManager.instance.SignUp(username.text, password.text);
    }
    public async void SignIn()
    {
        await AuthManager.instance.SignIn(username.text, password.text);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void IsLoggedIn()
    {
        GameManager.instance.LoadScene("Start");
    }
    public void ShowNotification(string e)
    {
        if (this == null) return;
        if (e != null)
        {
            StartCoroutine(NotificationTime(e));
        }
        else
        {

            StartCoroutine(NotificationTime("Error signup or signin"));
        }
    }
    IEnumerator NotificationTime(string errorString)
    {
        notificationPanel.SetActive(true);
        notificationPanel.GetComponentInChildren<TMP_Text>().text = errorString;
        yield return new WaitForSeconds(8f);
        notificationPanel.SetActive(false);

    }

    private void OnDisable()
    {
        AuthenticationService.Instance.SignedOut -= () => { IsLoggedIn(); };
        AuthenticationService.Instance.SignInFailed -= (err) =>
        {
            ShowNotification(err.Message);
        };
        AuthManager.OnException -= ShowNotification;
        StopAllCoroutines();
    }
}
