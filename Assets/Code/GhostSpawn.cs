using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    public GameObject ghost;
    private float ghostY = 0f;
    private int ghostX = 0;
    private float timePeriod = 6f;
    private float currentTime = 0;

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Sounds"));

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timePeriod)
        {
            currentTime = 0;

            Vector2 pos;
            ghostY = Random.Range(-4f, 5f);
            ghostX = Random.Range(0, 2);

            if (ghostX == 0)
            {
                pos = new Vector2(-10f, ghostY);
            }
            else
            {
                pos = new Vector2(10f, ghostY);
            }
            if (Random.Range(0,4) == 0)
            {
                GameObject.Find("Sounds").GetComponents<AudioSource>()[2].Play();
            }
            Instantiate(this.ghost, pos, Quaternion.identity);
        }
    }//Update

    public void ReduceTimePeriod()
    {
        timePeriod -= 0.15f;
    }
}
