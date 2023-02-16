using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GhostMovement : MonoBehaviour
{
    private float speed = 0;
    private GameObject liveText = null;
    private GameObject player = null;
    private GameObject sounds = null;

    void Start()
    {
        if (this.GetComponent<Transform>().position.x < 0)
        {
            this.GetComponent<Animator>().SetBool("WalkRight", true);
        }
        speed = Random.Range(1f, 4f);
        liveText = GameObject.Find("LiveValue");
        player = GameObject.Find("Sorceress");
        sounds = GameObject.Find("Sounds");

    }

    void Update()
    {
        //ghost movement
        if (this.GetComponent<Animator>().GetBool("WalkRight") == true)
        {
            this.GetComponent<Transform>().Translate(speed * Time.deltaTime, 0f, 0f);
            if (this.GetComponent<Transform>().position.x >= 10)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            this.GetComponent<Transform>().Translate(-speed * Time.deltaTime, 0f, 0f);
            if (this.GetComponent<Transform>().position.x <= -10)
            {
                Destroy(this.gameObject);
            }
        }
    }//Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Sorceress"))
        {
            if (!player.GetComponent<PlayerMovement>().GetImmunity())
            {
                liveText.GetComponent<Text>().text = (int.Parse(liveText.GetComponent<Text>().text) - 1).ToString();
                if (int.Parse(liveText.GetComponent<Text>().text) <= 0)
                {
                    sounds.GetComponents<AudioSource>()[4].Play();
                    SceneManager.LoadScene("GameOver");
                }
                sounds.GetComponents<AudioSource>()[3].Play();
                player.GetComponent<PlayerMovement>().SetImmunity(true);
            }
        }
    }
}
