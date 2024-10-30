using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.VFX;

public class CinemachineBehavior : MonoBehaviour
{
    
    private CinemachineVirtualCamera virtualCamera;
    private LockCameraY lockCam;
    public List<GameObject> players = new List<GameObject>(); //List to switch characters
    public GameObject playerPlaying;
    private GameObject target;
    public GameObject wolfBar;
    public GameObject catBar;

    // Start is called before the first frame update
    void Start()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));//get players for array

        //grab Cinemachine and lockCam
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        lockCam= gameObject.GetComponent<LockCameraY>();

        for(int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerScript>().playing)
            {
                virtualCamera.LookAt = players[i].transform;
                virtualCamera.Follow = players[i].transform;
                playerPlaying = players[i];
                break;
            }
        }

        //Assign Values to catCam
        if(playerPlaying.name == "Cat Player")
        {
            lockCam.m_YPosition = 7;    //set height of camera
            virtualCamera.m_Lens.OrthographicSize = 9;    //setsize of camera;
            catBar.SetActive(true);
            wolfBar.SetActive(false);
        }
        //Assign valyes to dogCam
        if(playerPlaying.name == "Wolf Player")
        {
            lockCam.m_YPosition = 8;    //set height of camera
            virtualCamera.m_Lens.OrthographicSize = 12;    //setsize of camera;
            catBar.SetActive(false);
            wolfBar.SetActive(true);
        }
    }

    public void CameraSet()
    {

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerScript>().playing)
            {
                players[i].GetComponent<PlayerScript>().playing = false;
                Debug.Log("Plaeyer " + players[i].name + "is active and being turned off");
            }
            else
            {
                Invoke("turningOn", 0.1f);
                target = players[i];
                playerPlaying = players[i];
                Debug.Log("Plaeyer " + players[i].name + "is not active and being turned on");
            }
        }
    }

    private void turningOn()
    {
        virtualCamera.LookAt = target.transform;
        virtualCamera.Follow = target.transform;
        target.GetComponent<PlayerScript>().playing = true;

        //Assign Values to catCam
        if (playerPlaying.name == "Cat Player")
        {
            lockCam.m_YPosition = 7;    //set height of camera
            virtualCamera.m_Lens.OrthographicSize = 9;    //setsize of camera;
            catBar.SetActive(true);
            wolfBar.SetActive(false);
        }
        //Assign valyes to dogCam
        if (playerPlaying.name == "Wolf Player")
        {
            lockCam.m_YPosition = 8;    //set height of camera
            virtualCamera.m_Lens.OrthographicSize = 12;    //setsize of camera;
            catBar.SetActive(false);
            wolfBar.SetActive(true);
        }
    }
}
