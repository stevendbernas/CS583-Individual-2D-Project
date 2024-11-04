using UnityEngine;

public class FastGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            car.speed *= 5; //gate that double the car speed
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            car.speed /= 5; //restore car speed after exiting
        }
    }
}
