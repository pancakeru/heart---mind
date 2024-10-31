using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cinemachine;
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

    [SerializeField] private Transform camTarget;
    private CinemachineVirtualCamera vCam;

    private float targetSize;
    private float camSpeed = 2;
    private float camTimer = 3;

    [SerializeField] float pushThreshold;

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

        vCam = GameObject.FindGameObjectWithTag("vCam").GetComponent<CinemachineVirtualCamera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        camTarget.position = Vector3.Lerp(camTarget.position, (catPlayer.transform.position + this.transform.position)/2, Time.deltaTime * camSpeed);

        if (catControl.angyLevel < pushThreshold ) {
            canPush = false;
            targetSize = 9;     
        } else {
            canPush = true;
            targetSize = 15;
        }

        if (canPush || !colliding) {
            rb.constraints = RigidbodyConstraints2D.None;

            camTimer -= 1 * Time.deltaTime;

            if (camTimer <= 0) {
                vCam.Follow = catPlayer.GetComponent<Transform>();
             }
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if (catPlayer.GetComponent<PlayerScript>().playing) {
             vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, targetSize, Time.deltaTime * camSpeed);
        }

    }


    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            colliding = true;
            camTimer = 3;
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && canPush) {
            vCam.Follow = camTarget;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            colliding = false;
        }
    }

}
