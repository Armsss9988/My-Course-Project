using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class AuthManager : MonoBehaviour
{

    public static AuthManager instance;
    string errorMessage;
    public static event Action<string> OnException;

    public string ErrorMessage => errorMessage;
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

    public async Task SignInCachedUserAsync()
    {
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            return;
        }
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (AuthenticationException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
    }
    public async Task SignIn(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("Signin success: " + username + " " + password);
        }
        catch (AuthenticationException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
    }
    public async Task SignUp(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("Signup success");
        }
        catch (AuthenticationException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
    }
    public void SignOut()
    {
        try
        {
            AuthenticationService.Instance.SignOut(true);
            Debug.Log("SignOut success");
        }
        catch (AuthenticationException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            OnException?.Invoke(ex.Message);
            Debug.LogException(ex);
        }
    }
}
