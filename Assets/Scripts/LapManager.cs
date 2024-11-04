using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LapManager : MonoBehaviour
{
    public TextMeshProUGUI lapCountText; //lap count 3/3
    public TextMeshProUGUI winnerText;   //winner blue red green car
    public TextMeshProUGUI lapTimeText;  //lap time in seconds
    public TextMeshProUGUI positionText; //1st 2nd 3rd place
    public TextMeshProUGUI endGameText;  //game over
    private int[] lapCounts;             //array to store lap counts
    private int totalLaps = 3;           //Total number of laps per race
    private int[] checkpointsPassed;     //array to count number of checkpoints passed for each car
    private float[] lapTimes;            //timer for current lap for each car
    private float totalTime;             //sum of lap times equal total race time
    private bool[] hasFinished;          //track each that finished race
    private List<int> finishOrder;       //display the order of cars when completed race
    private bool winnerDeclared;         //flag to indicate if a winner has been met

    private void Start()
    {
        lapCounts = new int[3] { totalLaps, totalLaps, totalLaps }; //intialize lap counts per car
        checkpointsPassed = new int[3] { 0, 0, 0 }; //initialize checkpoints passed per car
        lapTimes = new float[3] { 0f, 0f, 0f }; //initialize lap times per car
        hasFinished = new bool[3] { false, false, false }; //initialize finish status per car
        finishOrder = new List<int>(); //initialize finished order list per car
        winnerDeclared = false; //initialize winner declared to false

        UpdateLapCountText(); //initialize lap count display
        winnerText.gameObject.SetActive(false); //hide winner text at runtime
        lapTimeText.text = "Lap Time: 0.00s"; //initialize lap time display
        positionText.text = ""; //clear position text at runtime
        endGameText.gameObject.SetActive(false); //hide game over at runtime
    }

    private void Update()
    {
        for (int i = 0; i < lapCounts.Length; i++)
        {
            if (lapCounts[i] > 0 && !hasFinished[i]) //update lap time only if laps are remaining with cars that did not finish race
            {
                lapTimes[i] += Time.deltaTime; //increment lap time
                UpdateLapTimeText(i); //update lap time display per car
            }
        }


        if (AllCarsFinished()) //check if all cars finished race
        {
            EndGame(); //if all cars finished display game over
        }
    }

    public void DecrementLap(int carIndex)
    {
        if (carIndex < 0 || carIndex >= lapCounts.Length) //check if all cars decrement laps
            return; 

        if (lapCounts[carIndex] > 0 && checkpointsPassed[carIndex] >= 6) //check if car passed all checkpoints
        {
            totalTime += lapTimes[carIndex]; //add current lap time to total time per indexed car
            lapCounts[carIndex]--; //decrement lap count per indexed car
            checkpointsPassed[carIndex] = 0; //reset checkpoints per indexed car
            UpdateLapCountText(); //update lap count display

             if (lapCounts[carIndex] == 0 && !hasFinished[carIndex]) //check which car won
            {
                hasFinished[carIndex] = true; //mark indexed car as finished
                finishOrder.Add(carIndex); //add indexed car by finishing order

                // Only display the winner if no winner has been declared yet
                if (!winnerDeclared)
                {
                    winnerDeclared = true; // Set the winner flag to true
                    DisplayWinner(carIndex); // Call method to display winner
                }

                UpdatePositionText(); //update position display
            }

            lapTimes[carIndex] = 0f; //reset lap time for next lap
        }
    }

    public void RegisterCheckpoint(int carIndex)
    {
        if (carIndex < 0 || carIndex >= checkpointsPassed.Length)
            return; //check index bounds

        checkpointsPassed[carIndex]++; //increment checkpoint count for this car

    }

    public void FinishLineCrossed(int carIndex)
    {
        if (carIndex < 0 || carIndex >= lapCounts.Length)
            return; //check index bounds

        if (checkpointsPassed[carIndex] >= 6) //check if all checkpoints have been passed
        {
            DecrementLap(carIndex); //decrement lap count for indexed car
        }
    }

    private void DisplayWinner(int carIndex)
    {
        string carColor = GetCarColor(carIndex); //get car color
        winnerText.text = $"Winner: {carColor} Congratulations!\n\nTotal Time: {totalTime:F2}s"; //update winner text
        winnerText.gameObject.SetActive(true); //displacy winner text
    }

    private void UpdateLapCountText()
    {
        lapCountText.text = $"Lap Counts: Blue: {lapCounts[0]}/{totalLaps}, Green: {lapCounts[1]}/{totalLaps}, Red: {lapCounts[2]}/{totalLaps}";
    }

    private void UpdateLapTimeText(int carIndex)
    {
        string carColor = GetCarColor(carIndex);
        lapTimeText.text = $"Lap Time for {carColor}: {lapTimes[carIndex]:F2}s"; //update lap time display indexed car
    }

    private void UpdatePositionText()
    {
        positionText.text = "Final Positions:\n";
        for (int i = 0; i < finishOrder.Count; i++)
        {
            string carColor = GetCarColor(finishOrder[i]);
            string position = (i + 1).ToString(); //convert index to position
            switch (position)
            {
                case "1": position += "st"; break; //1st place
                case "2": position += "nd"; break; //2nd place
                case "3": position += "rd"; break; //3rd place
                default: position += "th"; break; //any other positions
            }
            positionText.text += $"\n{carColor}: {position} place\n"; //display car color after finishing race
        }
    }

    private bool AllCarsFinished()
    {

        foreach (bool finished in hasFinished) //check if all cars finished race
        {
            if (!finished)
                return false; //if any car did not finish race return false
        }
        return true; //all cars finished
    }

    private void EndGame()
    {
        endGameText.gameObject.SetActive(true); //display game over
        endGameText.text = "Game Over!";
        enabled = false;
    }

    private string GetCarColor(int carIndex)
    {
        switch (carIndex)
        {
            case 0: return "Blue"; //blue car
            case 1: return "Green"; //green car
            case 2: return "Red"; //red car
            default: return "Unknown"; //unknown car
        }
    }
}
