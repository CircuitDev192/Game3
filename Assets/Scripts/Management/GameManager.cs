using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Play,
    Paused,
    Credits
}

public class GameManager : Context<GameManager>
{
    #region States

    public GameMenuState menuState = new GameMenuState();
    public GamePlayState playState = new GamePlayState();
    public GamePauseState pauseState = new GamePauseState();
    public GameCreditsState creditsState = new GameCreditsState();

    #endregion

    #region Fields

    public static GameManager instance;

    [SerializeField] private GameObject[] managerPrefabs;
    private List<GameObject> managers;
    [SerializeField] private GameObject[] weaponPickupPrefabs;
    [SerializeField] private GameObject player;

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
        InitializeContext();

        EventManager.UIResumeClicked += UIResumeClicked;
        EventManager.UIQuitClicked += UIQuitClicked;
        EventManager.PlayerPickedUpWeapon += PlayerPickedUpWeapon;
    }

    private void Update()
    {
        ManageState(this);
    }

    public override void InitializeContext()
    {
        currentState = playState;
        currentState.EnterState(this);
    }

    private void PlayerPickedUpWeapon(string previousWeaponName)
    {
        foreach (GameObject pickup in weaponPickupPrefabs)
        {
            if (pickup.GetComponentInChildren<WeaponPickup>().weaponName == previousWeaponName)
            {
                float offset = 2f;
                GameObject weap = Instantiate(pickup, player.transform.position + (player.transform.forward * offset), Quaternion.identity);
                weap.GetComponentInChildren<Rigidbody>().AddForce(player.transform.forward * 300f);
                break;
            }
        }
    }

    #region Scene Methods

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

    #endregion

    #region UIEvents

    private void UIResumeClicked()
    {
        currentState.ExitState(this);

        currentState = playState;
        currentState.EnterState(this);
    }

    private void UIQuitClicked()
    {
        Application.Quit();
    }

    #endregion
}