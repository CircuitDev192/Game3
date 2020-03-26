using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject[] managerPrefabs;
    private List<GameObject> managers;

    #endregion

    private void Awake()
    {
        managers = new List<GameObject>();

        foreach (GameObject manager in managerPrefabs)
        {
            managers.Add(Instantiate(manager));
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }
}