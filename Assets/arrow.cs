using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.2f;
    public Vector3 pointA;
    public Vector3 pointB;

    void Start()
    {
        transform.position = new Vector3(2.2f, 3.1f, 0.5f);

        //pointA = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //pointB = new Vector3(transform.position.x ,transform.position.y + 0.12f, transform.position.z );
    }

    void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);
    }
}


