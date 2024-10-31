using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class MindMechanics : MonoBehaviour
{

    [SerializeField] private GameObject grower; 
    [SerializeField] private float rateOfChange;
    [SerializeField] private float initialSize;
    [SerializeField] private Transform camTarget;
    private CinemachineVirtualCamera vCam; 
    private float localCount; //local count keeps track of growth of specific wall, to make sure it doesn't go into the negative
    private static float limit = 120f;
    private float realPosChange;
    private float realScaleChange;
    private static float count; //keeps track of growth of all walls

    //ref to UI bar
    public Image buildBar;
    public Image dissolveBar;

    //inverse movement
    public bool inverse;

    //activated by player
    private bool canActivate = false;

    private GameObject[] players;
    private GameObject wolfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //grab sprites for skill bar
        buildBar = GameObject.FindGameObjectWithTag("angy bar").GetComponent<Image>();
        dissolveBar = GameObject.FindGameObjectWithTag("calm bar").GetComponent<Image>();

        //set the rate of growth based on changeable variables
        if (!inverse) {
            realPosChange = rateOfChange * 0.0645f;
            realScaleChange = rateOfChange * 0.129f;
        } else {
            realPosChange = rateOfChange * -0.0645f;
            realScaleChange = rateOfChange * -0.129f;
        }

        //set count and local count to 0
        count = 0f;
        localCount = 0f;

        //set initial height of wall
        for (int i = 0; i < initialSize; i++)
        {
            grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y + realPosChange);
            grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y + realScaleChange, grower.transform.localScale.z);
            localCount++;
        }

        //grab virtual camera reference
        vCam = GameObject.FindGameObjectWithTag("vCam").GetComponent<CinemachineVirtualCamera>();

        players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players) {
            if (player.GetComponent<HeartMechanics>() == null) {
                wolfPlayer = player;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //set visual of skill bar
        if(count >= 0)
        {
            buildBar.fillAmount = count / 120;
            dissolveBar.fillAmount = 0;
        }
        else
        {
            buildBar.fillAmount = 0;
            dissolveBar.fillAmount = -count / 120;
        }

        if (Input.GetKeyUp(KeyCode.L)) {
            wolfPlayer.GetComponent<Animator>().SetBool("L", false);
             }
        
          if (Input.GetKeyUp(KeyCode.P)) {
            wolfPlayer.GetComponent<Animator>().SetBool("P", false);
        }

        if (canActivate) {
            if (Input.GetKey(KeyCode.P)) {

                wolfPlayer.GetComponent<Animator>().SetBool("P", true);
                wolfPlayer.GetComponent<Animator>().SetBool("L", false);
                 if(count < limit)
                 {
                grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y + realPosChange);
                grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y + realScaleChange, grower.transform.localScale.z);                
                count++;
                localCount++;
                }
            }

            if (Input.GetKey(KeyCode.L)) {

                wolfPlayer.GetComponent<Animator>().SetBool("L", true);
                wolfPlayer.GetComponent<Animator>().SetBool("P", false);
                
                if(count > -limit && localCount > 0)
                 {
                grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y - realPosChange); 
                grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y - realScaleChange, grower.transform.localScale.z);
                count--;
                localCount--;
                 }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canActivate = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //make the camera target wall's camPosition
        if(collision.name == "Wolf Player" && collision.GetComponent<PlayerScript>().playing)
        {
            vCam.Follow = camTarget;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //return the camera to the player
        vCam.Follow = collision.gameObject.transform;
        canActivate = false;
    }

}
