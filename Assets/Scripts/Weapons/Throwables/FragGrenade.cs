using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : MonoBehaviour
{
    [SerializeField]
    private float timer = 5f;
    private bool isExploded = false;
    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !isExploded)
        {
            //explode
            Instantiate(explosion, this.gameObject.transform);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            isExploded = true;
            Destroy(this.gameObject, 4f);
        }
    }
}
