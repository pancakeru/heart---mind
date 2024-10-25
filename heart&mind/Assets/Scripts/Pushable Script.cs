using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PushableScript : MonoBehaviour
{
    
    private Rigidbody2D rb;

    private bool canPush = false;

    private GameObject[] players;
    private GameObject catPlayer;
    private HeartMechanics catControl;

    private bool colliding = false;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            HeartMechanics heartMechanics = player.GetComponent<HeartMechanics>();

            if (heartMechanics != null) {
                catPlayer = player;
                catControl = heartMechanics;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (catControl.angyLevel < 0.9f ) {
            canPush = false;
        } else {
            canPush = true;
        }

        if (canPush || !colliding) {
            rb.constraints = RigidbodyConstraints2D.None;
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }


    }


    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            colliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            colliding = false;
        }
    }

}
