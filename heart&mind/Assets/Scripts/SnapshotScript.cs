using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SnapshotScript : MonoBehaviour
{

    private VideoPlayer player;
    private bool started = false;
    public GameObject quit;
    public GameObject replay;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && player.isPlaying)
        {
            started = true;
        }
        if (started && !player.isPlaying)
        {
            if(SceneManager.GetActiveScene().buildIndex != 10)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                quit.SetActive(true);
                replay.SetActive(true);
            }
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
