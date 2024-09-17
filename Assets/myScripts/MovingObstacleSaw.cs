using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleSaw : MonoBehaviour
{

   
   [Range(0,200)]
   public float speed;
   
   Vector3 targetPos;

   public GameObject WaysSaws;
   public Transform[] wayPoints;
   int pointIndex;
   int pointCount;
   public float rotatespeed = 1;
   int direction = 1;

   

   private void Awake()
   {
    wayPoints = new Transform[WaysSaws.transform.childCount];
    for (int i = 0; i < WaysSaws.gameObject.transform.childCount; i++)
    {
        wayPoints[i] = WaysSaws.transform.GetChild(i).gameObject.transform;
    }
   }

   private void Start()
   {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
   }

   private void Update()
   {
        transform.Rotate(0,0,rotatespeed);
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (transform.position == targetPos)
        {
            NextPoint();
        }
    
   }

   void NextPoint()
   {
        if (pointIndex == pointCount - 1) //Arrived last point
        {
            direction = -1;
        }

        if (pointIndex == 0) //Arrived first point
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;

   }
}
