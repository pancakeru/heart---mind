using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMechanics : MonoBehaviour
{

    [SerializeField] private GameObject grower;
    [SerializeField] private float rateOfChange;
    private float realPosChange;
    private float realScaleChange;

    // Start is called before the first frame update
    void Start()
    {
        realPosChange = rateOfChange * 0.0645f;
        realScaleChange = rateOfChange * 0.129f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey(KeyCode.O))
        {
            grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y + realPosChange);
            grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y + realScaleChange, grower.transform.localScale.z);
        }
    }

}
