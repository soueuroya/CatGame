using System.Collections;
using UnityEngine;

public class MovingObstacleSpike : MonoBehaviour
{
   [Range(0,50)]
   public float speed;
   [Range(0,20)]
   public float waitDuration;
   Vector3 targetPos;
   public GameObject WaysSpikes;
   public Transform[] wayPoints;
   int pointIndex;
   int pointCount;
   int direction = 1;
   int speedMultiplier = 1;

   private void Awake()
   {
    wayPoints = new Transform[WaysSpikes.transform.childCount];
    for (int i = 0; i < WaysSpikes.gameObject.transform.childCount; i++)
    {
        wayPoints[i] = WaysSpikes.transform.GetChild(i).gameObject.transform;
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
        var step = speedMultiplier*speed * Time.deltaTime;
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
        StartCoroutine(WaitNextPoint());
   }
   IEnumerator WaitNextPoint()
   {
    speedMultiplier = 0;
    yield return new WaitForSeconds(waitDuration);
    speedMultiplier = 1;
   }
}
