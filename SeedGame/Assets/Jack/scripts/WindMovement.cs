using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    public Vector2 windDir;
    Rigidbody2D rb;

    Vector3[] points = new Vector3[20];
    Vector3[] windDirs = new Vector3[20];
    float delayTime;
    bool donePoints;
    bool swoosh;
    int furtherstPoint;
    int totalPoints;

    bool method;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = new Vector2(0,0);
        endPos = new Vector2(0, 0);
        windDir = new Vector2(0, 0);
        donePoints = false;
        swoosh = false;
        furtherstPoint = 0;
        totalPoints = 0;

        delayTime = 0.05f;
        method = true;//set to false for method 2, not working rn
    }

    // Update is called once per frame
    void Update()
    {
        //Method 1 - one line from start point to end
        if (method)
        {
            //get wind start point
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;  
            }

            //get wind end point and set new wind direction to vector between start and end
            if (Input.GetMouseButtonUp(0))
            {
                endPos = Input.mousePosition;
                windDir = (endPos - startPos) / 20;
            }

            //reduce wind to 0 over time
            windDir -= windDir.normalized * new Vector2(0.1f, 0.1f);
            Debug.DrawLine(startPos, endPos, Color.yellow, 10);

        }






        //Method 2 - curve lines along 5 points, first point is start point, last is end point
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //set start point
                points[0] = Input.mousePosition;

                //get mid points
                swoosh = false;
                donePoints = false;
                StartCoroutine(PointCount()); 
            }


            if (Input.GetMouseButtonUp(0))
            {
                //cut off threads when mouse released
                totalPoints = furtherstPoint;
                if (totalPoints > points.Length) totalPoints = points.Length;

                //set end pos
                points[furtherstPoint] = Input.mousePosition;

                //set mid points
                donePoints = true;
                swoosh = true;
                StartCoroutine(MoveWind());
            }
        }
        //move leaf
        rb.AddForce(new Vector2(windDir.x, windDir.y));
    }



    IEnumerator PointCount()
    {
        furtherstPoint = 1;
        //get 3 points between start and end
        for (int i = 1; i < points.Length - 1; i++)
        {
            if (donePoints == false)
            {
                yield return new WaitForSeconds(delayTime);
                points[i] = Input.mousePosition;
                furtherstPoint++;
            }
        } 
    }

    IEnumerator MoveWind()
    {
        yield return new WaitForSeconds(delayTime);
        //set mid points
        for (int i = 1; i < totalPoints; i++)
        {
            windDirs[i - 1] = points[i] - points[i - 1];
        }



        //move rigid body
        for (int i = 0; i < totalPoints; i++)
        {
            if (swoosh)
            {
                yield return new WaitForSeconds(delayTime);
                rb.AddForce(new Vector2(windDirs[i].x, windDirs[i].y));

                Debug.Log("Moved " + i);
                Debug.DrawLine(points[i], points[i + 1], Color.red, 10);
            }   
        }
    }
}
