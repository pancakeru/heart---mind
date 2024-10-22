using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineBehavior : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();

        virtualCamera.LookAt =  GameObject.FindWithTag("Player").transform;
        virtualCamera.Follow = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
