using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private int lapCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
		
        if (other.CompareTag("Car")) //if "Car" "Car2" "Car2" crosses finish line then increment lap
        {
            lapCount++;
        }
    }
}
