using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursor;

    private int zombiesEliminated;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2f, cursor.height / 2f), CursorMode.Auto);
        zombiesEliminated = 0;

        EventManager.ZombieKilled += ZombieEliminated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void ZombieEliminated()
    {
        zombiesEliminated++;
        EventManager.TriggerScoreChanged(zombiesEliminated);
    }
}
