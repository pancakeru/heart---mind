using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{

    public List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //players.Add(GameObject.FindGameObjectsWithTag("Player")
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
