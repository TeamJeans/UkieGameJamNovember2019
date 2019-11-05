using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMoveTest : MonoBehaviour
{

    Rigidbody2D rb;
    public Vector2 windFactor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        windFactor = GetComponent<WindMovement>().windDir;
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Debug.Log("GO RIGHT");
            rb.AddForce(new Vector2(-100, 100));
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            //Debug.Log("GO LEFT");
            rb.AddForce(new Vector2(-100, 100));
        }



        rb.AddForce(new Vector2(windFactor.x, windFactor.y));
    }
}
