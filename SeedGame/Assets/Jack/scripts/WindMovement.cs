using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    public Vector2 windDir;


    Vector3[] points = new Vector3[5];
    Vector3[] windDirs = new Vector3[5];
    int nextPoint;
    bool donePoints;

    bool method;//TODO DELETE THIS

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(0,0);
        endPos = new Vector2(0, 0);
        windDir = new Vector2(0, 0);
        method = true;//TODO DELETE THIS
        nextPoint = 0;
        donePoints = false;
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

        }






        //Method 2 - curve lines along 5 points, first point is start point, last is end point
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                points[0] = Input.mousePosition;
                nextPoint = 0;
                donePoints = false;
                StartCoroutine(PointCount());
            }


            if (Input.GetMouseButtonUp(0))
            {
                donePoints = true;
                points[5] = Input.mousePosition;
                windDir = (endPos - startPos) / 20;       
            }
        }
    }



    IEnumerator PointCount()
    {
        //get 3 points between start and end
        for (int i = 1; i < points.Length - 2; i++)
        {
            yield return new WaitForSeconds(0.2f);
            points[nextPoint] = Input.mousePosition;
            nextPoint++;
        }
    }

    IEnumerator MoveWind()
    {

        for (int i = 0; i < points.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
        }
        
    }




}
