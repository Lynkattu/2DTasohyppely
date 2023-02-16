using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //live text UI game object
    private GameObject liveText = null;
    private GameObject sounds = null;
    //damage immunity
    private bool isImmunie = false;
    private float immunityTime = 0f;
    //player velocity
    private float acceleration = 5;
    private float velocity = 0;
    private float maxVelocity = 3.5f;
    private float startingVelocity = 1f;
    //jump force
    private Vector2 jumpForce = new Vector2(0f,2400f);
    //states
    private bool isJumping = false;
    //player movement control keys
    private KeyCode moveLeft = KeyCode.LeftArrow;
    private KeyCode moveRight = KeyCode.RightArrow;
    private KeyCode jump = KeyCode.Space;


    void Start()
    {
        //find UI game object
        liveText = GameObject.Find("LiveValue");
        sounds = GameObject.Find("Sounds");
    }

    void Update()
    {

        //right move animation
        if (Input.GetKeyUp(moveRight))
        {
            if (!isJumping)
            {
                this.GetComponent<Animator>().SetInteger("state", 0);
            }
            velocity = startingVelocity;

        }

        //left move animation
        if (Input.GetKeyUp(moveLeft))
        {
            if (!isJumping)
            {
                this.GetComponent<Animator>().SetInteger("state", 3);
            }
            velocity = startingVelocity;
        }

        if (Input.GetKey(moveRight) && Input.GetKey(moveLeft))
        {
            if (this.GetComponent<Animator>().GetInteger("state") == 1)
            {
                this.GetComponent<Animator>().SetInteger("state", 0);
            }
            if (this.GetComponent<Animator>().GetInteger("state") == 2)
            {
                this.GetComponent<Animator>().SetInteger("state", 3);
            }
        }
        else
        {
            //move right
            if (Input.GetKey(moveRight))
            {
                if (!isJumping)
                {
                    this.GetComponent<Animator>().SetInteger("state", 1);
                }
                else
                {
                    this.GetComponent<Animator>().SetInteger("state", 4);
                }

                if (velocity < maxVelocity)
                {
                    velocity += acceleration * Time.deltaTime;
                    if (velocity > maxVelocity)
                    {
                        velocity = maxVelocity;
                    }
                }
                this.GetComponent<Transform>().Translate(velocity * Time.deltaTime, 0f, 0f);
            }
            //move left
            else if (Input.GetKey(moveLeft))
            {
                if (!isJumping)
                {
                    this.GetComponent<Animator>().SetInteger("state", 2);
                }
                else
                {
                    this.GetComponent<Animator>().SetInteger("state", 5);
                }

                if (velocity < maxVelocity)
                {
                    velocity += acceleration * Time.deltaTime;
                    if (velocity > maxVelocity)
                    {
                        velocity = maxVelocity;
                    }
                }
                this.GetComponent<Transform>().Translate(-velocity * Time.deltaTime, 0f, 0f);
            }
        }

        //jump
        if (Input.GetKeyDown(jump) && !isJumping)
        {
            if (this.GetComponent<Animator>().GetInteger("state") == 0 || this.GetComponent<Animator>().GetInteger("state") == 1)
            {
                this.GetComponent<Animator>().SetInteger("state", 4);
            }
            else if (this.GetComponent<Animator>().GetInteger("state") == 3 || this.GetComponent<Animator>().GetInteger("state") == 2)
            {
                this.GetComponent<Animator>().SetInteger("state", 5);
            }
            this.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        }

        //check fall out of game area
        if (this.GetComponent<Transform>().position.y <= -8 && int.Parse(liveText.GetComponent<Text>().text) > 0)
        {
            liveText.GetComponent<Text>().text = (int.Parse(liveText.GetComponent<Text>().text) - 1).ToString();
            if (int.Parse(liveText.GetComponent<Text>().text) > 0)
            {
                sounds.GetComponents<AudioSource>()[3].Play();
                this.GetComponent<Transform>().position = new Vector2(-7.54f, -3.70f);
                this.isImmunie = true;
            }
            else
            {
                sounds.GetComponents<AudioSource>()[4].Play();
                SceneManager.LoadScene("GameOver");
            }
        }

        //damage immunity (non fall damage)
        if (isImmunie)
        {
            Color c = new Color(1f, 1f, 1f, 0.5f);
            this.GetComponent<SpriteRenderer>().color = c;
            immunityTime += 1 * Time.deltaTime;
            if (immunityTime >= 3)
            {
                c = new Color(1, 1, 1, 1);
                this.GetComponent<SpriteRenderer>().color = c;
                isImmunie = false;
                immunityTime = 0;
            }
        }

        //quit application
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }//update

    //set jumping state
    public void SetJumping(bool jumping)
    {
        isJumping = jumping;
    }

    //get immunity state
    public bool GetImmunity()
    {
        return isImmunie;
    }

    //set immunity state
    public void SetImmunity(bool immunityState)
    {
        isImmunie = immunityState;
        immunityTime = 0;
        if (!immunityState)
        {
            Color c = new Color(1f, 1f, 1f, 1f);
            this.GetComponent<SpriteRenderer>().color = c;
        }
    }


}
