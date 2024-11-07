using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThroughBox : MonoBehaviour
{

    //private Collider2D colliderTrigger;
    public Collider2D colliderBox;

    // Start is called before the first frame update
    void Start()
    {
        //colliderTrigger = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.transform.position.y + 2 > collision.transform.position.y)
        {
            colliderBox.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliderBox.enabled=true;
    }

}
