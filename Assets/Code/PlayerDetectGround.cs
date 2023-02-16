using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectGround : MonoBehaviour
{
    private GameObject parent = null;
    void Start()
    {
        parent = GameObject.Find("Sorceress");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        parent.GetComponent<PlayerMovement>().SetJumping(false);
        if (parent.GetComponentInParent<Animator>().GetInteger("state") == 4)
        {
            parent.GetComponentInParent<Animator>().SetInteger("state", 0);
        }

        if (parent.GetComponentInParent<Animator>().GetInteger("state") == 5)
        {
            parent.GetComponentInParent<Animator>().SetInteger("state", 3);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        parent.GetComponent<PlayerMovement>().SetJumping(true);
    }
}
