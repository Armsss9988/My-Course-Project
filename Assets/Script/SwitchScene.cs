using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] string switchName;
    [SerializeField] string targetName;
    [SerializeField] string sceneChange;
    Transform startPoint;


    public string Name => switchName;

    private void Awake()
    {
        startPoint = this.transform.Find("StartPoint");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.TryGetComponent<Character>(out var player))
            {
                StartCoroutine(ChangeScene());
            }
        }

    }
    IEnumerator ChangeScene()
    {
        LoadingScreenUI.instance.OpenLoading();
        bool isSceneLoaded = SceneManager.GetSceneByName(sceneChange).isLoaded;

        if (isSceneLoaded)
        {
            Debug.Log("scene" + sceneChange + "is loaded");
            yield return SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneChange));
        }
        else
        {
            yield return SceneManager.LoadSceneAsync(sceneChange, LoadSceneMode.Additive);
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SwitchScene[] portals = GameObject.FindObjectsOfType<SwitchScene>();
        foreach (SwitchScene switchScene in portals)
        {
            if (switchScene != null && switchScene.Name == targetName)
            {
                player.transform.position = switchScene.startPoint.position;

                break;
            }
        }
        LoadingScreenUI.instance.CloseLoading();
    }

}
