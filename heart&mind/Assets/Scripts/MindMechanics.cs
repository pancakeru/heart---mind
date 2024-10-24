using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMechanics : MonoBehaviour
{

    [SerializeField] private GameObject grower;
    [SerializeField] private float rateOfChange;
    [SerializeField] private float initialSize;
    private float localCount;
    private static float limit = 120f;
    private float realPosChange;
    private float realScaleChange;
    private static float count;

    // Start is called before the first frame update
    void Start()
    {
        realPosChange = rateOfChange * 0.0645f;
        realScaleChange = rateOfChange * 0.129f;

        count = 0f;
        localCount = 0f;

        for (int i = 0; i < initialSize; i++)
        {
            grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y + realPosChange);
            grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y + realScaleChange, grower.transform.localScale.z);
            localCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey(KeyCode.P))
        {
            if(count < limit)
            {
                grower.transform.position = new Vector3(grower.transform.position.x, grower.transform.position.y + realPosChange);
                grower.transform.localScale = new Vector3(grower.transform.localScale.x, grower.transform.localScale.y + realScaleChange, grower.transform.localScale.z);                
                count++;
                localCount++;
            }
        }
        if (collision.tag == "Player" && Input.GetKey(KeyCode.L))
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

}
