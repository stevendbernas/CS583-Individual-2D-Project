using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Speed and movement parameters
	private int currentWaypointIndex = 0; //current waypoint index AI car is targeting
    private Rigidbody2D rb;               //reference to Rigidbody2D component for movement
    private float currentSpeed = 0f;      //current speed of AI car
    public float turnSmoothness = 0.1f;   //how smooth turns are
    public float waypointRadius = 1f;     //how close AI car needs to be to switch to next waypoint
    public float stoppingDistance = 0.5f; //distance to slow down before reaching waypoint
    public Transform[] waypoints;         //list of waypoints for AI car to follow
    public float speed = 5f;              //normal speed of AI car
    public float maxSpeed = 8f;           //fastest speed AI car can go
    public float acceleration = 2f;       //how quickly AI car speeds up
    public float deceleration = 4f;       //how quickly AI car slows down
    public float turnSpeed = 200f;        //speed at which AI car turns towards waypoint

    void FixedUpdate()
    {
		if (waypoints?.Length == 0) return; //is waypoint null

        Transform targetWaypoint = waypoints[currentWaypointIndex]; //get current waypoint
        

        float distanceToWaypoint = Vector2.Distance(transform.position, targetWaypoint.position); //distance to waypoint


        if (distanceToWaypoint > stoppingDistance) //adjust speed based on distance to waypoint
        {

            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed); //if waypoint far speed up
        }
        else
        {

            currentSpeed = Mathf.Max(currentSpeed - deceleration * Time.fixedDeltaTime, 0f); //if waypoint close slow down
        }

  
        rb.velocity = transform.up * currentSpeed; //move AI car forward in the direction it is facing

		if (distanceToWaypoint < waypointRadius) //check if AI car is close enough to current waypoint
		{

		currentWaypointIndex++; //move to next waypoint then increment current waypoint index

		if (currentWaypointIndex >= waypoints.Length) //check if index exceeds number of waypoints
		{

        currentWaypointIndex = 0; //if exceeds number waypoints reset index to 0 and loop to start
    }
}

        Vector2 direction = (targetWaypoint.position - transform.position).normalized; //calculate direction to target waypoint

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; //calculate target angle to face waypoint

        float currentTurnSpeed = Mathf.Lerp(currentSpeed, turnSpeed, turnSmoothness); //smoothly rotate AI car towards target angle
        float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, currentTurnSpeed * Time.fixedDeltaTime);
        rb.rotation = angle;
    }

    void Start()
    {

        rb = GetComponent<Rigidbody2D>(); //get Rigidbody2D component attached to AI object
    }


    public float GetCurrentSpeed() //get current speed of AI car
    {
        return currentSpeed;
    }
}
