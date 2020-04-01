using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Context<GameManager>
{
    #region Fields

    public static GameManager instance;

    [SerializeField] private GameObject[] managerPrefabs;
    private List<GameObject> managers;

    private string sceneToLoad;
    private string sceneToUnLoad;

    #endregion

    private void Awake()
    {
        instance = this;

        managers = new List<GameObject>();

        foreach (GameObject manager in managerPrefabs)
        {
            managers.Add(Instantiate(manager, this.transform));
        }

        DontDestroyOnLoad(this);
    }

    public override void InitializeContext()
    {
        throw new System.NotImplementedException();
    }

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOp.completed += SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperation obj)
    {
        EventManager.TriggerSceneLoaded(sceneToLoad);
    }

    public void UnLoadScene(string sceneName)
    {
        sceneToUnLoad = sceneName;
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);
        unloadOp.completed += SceneUnLoadCompleted;
    }

    private void SceneUnLoadCompleted(AsyncOperation obj)
    {
        EventManager.TriggerSceneUnLoaded(sceneToUnLoad);
    }
}