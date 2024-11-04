using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    private LapManager lapManager;
    private Dictionary<string, int> carIndexMap;
    private HashSet<string> carsCrossed = new HashSet<string>(); //track cars that crossed

    private void Start()
    {
        lapManager = FindObjectOfType<LapManager>(); //find LapManager in the scene
        InitializeCarIndexMap(); //initialize dictionary
    }

    private void InitializeCarIndexMap()
    {
        carIndexMap = new Dictionary<string, int>
        {
            { "Car", 0 },   //Car index
            { "Car2", 1 },  //Car2 index
            { "Car3", 2 }   //Car3 index
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (carIndexMap.ContainsKey(other.tag)) //check if object that entered trigger is tagged "Car" "Car2" "Car3"
        {
            string carTag = other.tag;
            if (!carsCrossed.Contains(carTag)) //check if certain car already crossed
            {
                carsCrossed.Add(carTag); //track certain car that crossed
                int carIndex = carIndexMap[carTag]; //get car index from dictionary
                lapManager.DecrementLap(carIndex); //pass car index to decrement the lap
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (carIndexMap.ContainsKey(other.tag)) //reset crossed state when car exits trigger
        {
            carsCrossed.Remove(other.tag); //allow car to cross again in the future
        }
    }
}
