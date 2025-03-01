using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource idleSource;        //reference to idle audio source
    public AudioSource revSource;         //reference to rev audio source
    public float speedThreshold = 10f;    //speed at which to start reving sound
    public float maxPitch = 2f;           //maximum pitch for rev sound
    public float minPitch = 1f;           //minimum pitch when idle
    public float pitchChangeRate = 1f;    //how fast pitch changes
    private CarController carController;  //reference to CarController
    private AIController aiController;    //reference to AIController

    private void Start()
    {
        carController = GetComponent<CarController>(); //get CarController attached to GameObject
        aiController = GetComponent<AIController>(); //get AIController attached to GameObject
    }

    private void Update()
    {
        float speed = GetCurrentSpeed();

        if (speed > speedThreshold)
        {

            if (!revSource.isPlaying) //only play rev sound if car is moving
            {
                revSource.Play();
            }

            float pitch = Mathf.Lerp(minPitch, maxPitch, speed / 100f); //calculate pitch based on speed to adjust denominator based on speed scale
            revSource.pitch = Mathf.MoveTowards(revSource.pitch, pitch, pitchChangeRate * Time.deltaTime);
        }
        else
        {
            if (revSource.isPlaying) //if car is not moving stop the rev sound and play idle sound
            {
                revSource.Stop();
            }

            if (!idleSource.isPlaying) //play idle sound
            {
                idleSource.Play();
            }

            revSource.pitch = minPitch; //reset pitch to minPitch when idle
        }
    }

    private float GetCurrentSpeed()
    {
        if (carController != null) //determine which controller is being used then return current speed
        {
            return carController.GetCurrentSpeed(); //calls CarController method
        }
        else if (aiController != null)
        {
            return aiController.GetCurrentSpeed(); //calls AIController method
        }
        return 0f; //default set to 0 if no controller is found
    }
}
