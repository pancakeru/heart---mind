using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {
       if (other.gameObject.CompareTag("Player")) {
            LoadNext();
       }
    }

    public void LoadNext() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
