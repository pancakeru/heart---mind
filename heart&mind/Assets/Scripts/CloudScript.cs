using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float leftBound;
    public float rightBound;

    public bool right;

    private float speed;
    private float size;

    void Start()
    {
        speed = Random.Range(3, 10);
        size = Random.Range(1, 3);

        if (!right) {
            speed *= -1;
        }

        this.transform.localScale = new Vector3(size, size, 120);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        if (this.transform.position.x < leftBound) {
            this.transform.position = new Vector3(rightBound, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x > rightBound) {
            this.transform.position = new Vector3(leftBound, this.transform.position.y, this.transform.position.z);
        }

    }
}
