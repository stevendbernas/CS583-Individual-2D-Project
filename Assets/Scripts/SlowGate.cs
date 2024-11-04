using UnityEngine;

public class SlowGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            car.speed /= 2; //gate that slows car speed by half
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            car.speed *= 2; //restores default car speed
        }
    }
}
