using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineBehavior : MonoBehaviour
{
    
    private CinemachineVirtualCamera virtualCamera;
    private LockCameraY lockCam;

    // Start is called before the first frame update
    void Start()
    {
        //grab Cinemachine and lockCam
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        lockCam= gameObject.GetComponent<LockCameraY>();

        GameObject player = GameObject.FindWithTag("Player");

        //Grab Player Object
        virtualCamera.LookAt =  player.transform;
        virtualCamera.Follow = player.transform;

        //Assign Values to catCam
        if(player.name == "Cat Player")
        {
            lockCam.m_YPosition = 10;    //set height of camera
            virtualCamera.m_Lens.OrthographicSize = 15;    //setsize of camera;
        }
        //Assign valyes to dogCam
        if(player.name == "Wolf Player")
        {
            lockCam.m_YPosition = 20;    //set height of camera
            virtualCamera.m_Lens.OrthographicSize = 25;    //setsize of camera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
