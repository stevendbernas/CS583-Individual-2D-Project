using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    private LapManager lapManager;

    private void Start()
    {
        lapManager = FindObjectOfType<LapManager>(); //find LapManager in the scene
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Car2") || other.CompareTag("Car3")) //check if object that entered trigger is tagged "Car" "Car2" "Car3"
        {
            int carIndex = GetCarIndex(other.gameObject.tag); //get car index based on tag
            lapManager.RegisterCheckpoint(carIndex); //call RegisterCheckpoint method with car index
        }
    }

    private int GetCarIndex(string carTag) //helper method to get car index based on tag
    {
        switch (carTag)
        {
            case "Car":
                return 0; //Car index
            case "Car2":
                return 1; //Car2 index
            case "Car3":
                return 2; //Car3 index
            default:
                return -1; //invalid index
        }
    }
}
