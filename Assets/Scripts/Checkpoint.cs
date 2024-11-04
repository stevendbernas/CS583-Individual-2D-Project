using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LapManager lapManager; 
    private int carIndex;          //intialize car index

    private void Start()
    {
        lapManager = FindObjectOfType<LapManager>(); //get reference to LapManager
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Car")) //check if object that entered trigger is tagged "Car" "Car2" "Car3"
        {
            carIndex = 0; //index for Car
            lapManager.RegisterCheckpoint(carIndex); //pass the car index
        }
        else if (other.CompareTag("Car2"))
        {
            carIndex = 1; //index for Car2
            lapManager.RegisterCheckpoint(carIndex); //pass the car index
        }
        else if (other.CompareTag("Car3"))
        {
            carIndex = 2; //index for Car3
            lapManager.RegisterCheckpoint(carIndex); //pass the car index
        }
    }
}
