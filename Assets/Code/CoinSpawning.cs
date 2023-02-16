using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    public GameObject coin;
    public AudioClip goldPick;

    void Start()
    {
        //spawn coin in random location
        int position = Random.Range(0, this.transform.childCount);
        GameObject spawnedCoin = Instantiate(this.coin);
        spawnedCoin.GetComponent<Transform>().position = this.GetComponent<Transform>().GetChild(position).position;
    }

    void Update()
    {
        
    }

    public void spawnCoin()
    {
        //spawn coin in random location
        int position = Random.Range(0, this.transform.childCount);
        GameObject spawnedCoin = Instantiate(this.coin);
        spawnedCoin.GetComponent<Transform>().position = this.GetComponent<Transform>().GetChild(position).position;
    }

}
