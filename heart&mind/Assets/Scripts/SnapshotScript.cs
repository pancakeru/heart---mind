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

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Snapshot 1.mp4");
            player.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Snapshot 2.mp4");
            player.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Snapshot 3.mp4");
            player.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Snapshot 4.mp4");
            player.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Snapshot 5.mp4");
            player.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Snapshot 6.mp4");
            player.Play();
        }
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
