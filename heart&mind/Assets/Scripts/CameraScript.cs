using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //reference to the player in the scene
    private GameObject playerObj;
    private Transform playerPos;

    //camera y position control var
    private float yPos;
    private float yDiff; //store initial y pos offset between camera and player

    //var to determine when to adjust y pos
    private float yOffset = 3f;
    private float lerpSpeed = 2f; //lerp speed to adjust camera

    void Start()
    {
        //depending on the lvl it will ref the cat or wolf
        playerObj = GameObject.FindWithTag("Player");
        playerPos = playerObj.GetComponent<Transform>();

        //store initial y position
        yPos = this.transform.position.y;

        //store initial y pos difference between camera and player
        yDiff = yPos - playerPos.position.y; 
        
    }

    void Update()
    {
        //if the player y is higher or lower than a certain point relative to camera
        if (playerPos.position.y < yPos- yOffset || playerPos.position.y > yPos + yOffset) {
            //make the y pos lerp towards the new pos of the player and maintain initial offset
             yPos = Mathf.Lerp(yPos, playerPos.position.y + yDiff, Time.deltaTime * lerpSpeed);
        }

        //update position
        this.transform.position = new Vector3(playerPos.position.x, yPos, this.transform.position.z);

    }
}
