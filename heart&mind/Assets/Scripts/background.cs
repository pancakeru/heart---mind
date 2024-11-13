using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    // Start is called before the first frame update

   public float speed;
   private GameObject[] playerObj;

   void Start() {
    playerObj = GameObject.FindGameObjectsWithTag("Player");

   }

   void Update() {
    foreach(GameObject player in playerObj) {
        if (Input.GetKey(KeyCode.D)) {
            this.transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)) {
            this.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }

    }

   }

}
