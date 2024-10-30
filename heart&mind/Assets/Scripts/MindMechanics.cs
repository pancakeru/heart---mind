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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //make the camera target wall's camPosition
        if(collision.name == "Wolf Player")
        {
            vCam.Follow = camTarget;
        }

        //make wall grow
        if(collision.name == "Wolf Player" && Input.GetKey(KeyCode.P))
        {
            if(count < limit)
            {
                grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y + realPosChange);
                grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y + realScaleChange, grower.transform.localScale.z);                
                count++;
                localCount++;
            }
        } //make wal go down
        if (collision.name == "Wolf Player" && Input.GetKey(KeyCode.L))
        {
            if(count > -limit && localCount > 0)
            {
                grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y - realPosChange); 
                grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y - realScaleChange, grower.transform.localScale.z);
                count--;
                localCount--;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //return the camera to the player
        vCam.Follow = collision.gameObject.transform;
    }

}
