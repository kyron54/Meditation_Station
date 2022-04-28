using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingCamera : MonoBehaviour
{
    int currentPoint;
    float destTime;
    Vector3 oldPos;
    Quaternion oldRot;
    public Transform waypointsParent;
    List<CameraWaypoint> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = new List<CameraWaypoint>();
        foreach(Transform child in waypointsParent)
        {
            CameraWaypoint waypoint = child.GetComponent<CameraWaypoint>();
            waypoints.Add(waypoint);
        }

        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float t = 1f - ((destTime - Time.time) / waypoints[currentPoint].time);
        transform.position = Vector3.Lerp(oldPos, waypoints[currentPoint].transform.position, t);
        transform.rotation = Quaternion.Lerp(oldRot, waypoints[currentPoint].transform.rotation, t);

        if (t >= 1)
            NextPoint();
    }

    void NextPoint()
    {

        currentPoint++;
        if (currentPoint >= waypoints.Count)
            ReloadGarden();
        destTime = Time.time + waypoints[currentPoint].time;
        oldPos = transform.position;
        oldRot = transform.rotation;
    }

    public void ReloadGarden()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("JungleWorld");
    }
}
