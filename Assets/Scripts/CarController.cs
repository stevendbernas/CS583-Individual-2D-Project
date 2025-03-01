using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5f;            //car speed
    public float rotationSpeed = 200f;  //rotation speed of car
    public AudioSource engineSound;     //reference to audio source for engine sound
    private float currentSpeed = 0f;    //variable to store current speed

    private void Update()
    {
        float moveInput = Input.GetAxis("Vertical"); //get input for moving forward or backwards
        float turnInput = Input.GetAxis("Horizontal"); //get input for moving forward or backwards

        transform.Translate(Vector2.up * moveInput * speed * Time.deltaTime); //move car forward or backwards

        transform.Rotate(Vector3.forward, -turnInput * rotationSpeed * Time.deltaTime); //rotate car

        currentSpeed = Mathf.Abs(moveInput * speed); //update current speed based on input

        AdjustEngineSoundPitch(); //adjust engine sound pitch based on speed
    }

    private void AdjustEngineSoundPitch()
    {

        float normalizedSpeed = Mathf.Clamp(currentSpeed / speed, 0, 1); //normalize speed to a value between 0 and 1

        engineSound.pitch = 1 + normalizedSpeed * 2; //adjust pitch based on normalized speed
    }

    public float GetCurrentSpeed() //get current speed
    {
        return currentSpeed;
    }
}
