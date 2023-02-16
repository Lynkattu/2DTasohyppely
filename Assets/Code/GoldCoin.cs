using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoin : MonoBehaviour
{
    private GameObject coinSpawn = null;
    private GameObject goldText = null;
    private GameObject codeHolder = null;

    // Start is called before the first frame update
    void Start()
    {
       coinSpawn = GameObject.Find("CoinSpawn");
       goldText = GameObject.Find("GoldValue");
       codeHolder = GameObject.Find("CodeHolder");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Sorceress"))
        {
            
            GameObject.Find("Sounds").GetComponents<AudioSource>()[1].Play();
            Destroy(this.gameObject);
            this.goldText.GetComponent<Text>().text = (int.Parse(this.goldText.GetComponent<Text>().text) + 1).ToString();
            this.coinSpawn.GetComponent<CoinSpawning>().spawnCoin();
            this.codeHolder.GetComponent<GhostSpawn>().ReduceTimePeriod();
        }
    }
}
