using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public float sphereRadius = 1f; //radius of waypoint spheres
    public Color waypointColor = Color.red; //color for waypoints for design
    public Color lineColor = Color.blue; //color for lines between waypoints to indentify

    private List<Transform> waypointsList = new List<Transform>(); //list to store waypoints

    private void OnDrawGizmos()
    {
        Gizmos.color = waypointColor; //using Gizmo to set waypoint colors

        waypointsList.Clear(); //clear list of waypoints

        foreach (Transform t in transform) //loop through waypoint then draw a sphere of its position
        {
            waypointsList.Add(t); //add waypoint to list
            Gizmos.DrawWireSphere(t.position, sphereRadius);  //draw wire sphere at each waypoint position
        }

        Gizmos.color = lineColor; //set Gizmo color for lines connecting each waypoint

        for (int i = 0; i < waypointsList.Count - 1; i++) //draw lines between waypoints
        {
            Gizmos.DrawLine(waypointsList[i].position, waypointsList[i + 1].position);
        }
    }

    public Transform GetWaypoint(int index) //get waypoint by index
    {
        if (index >= 0 && index < waypointsList.Count)
        {
            return waypointsList[index];
        }
        return null; //return null if index out of bounds
    }

    public int GetWaypointCount() //get total number of waypoints
    {
        return waypointsList.Count;
    }
}
