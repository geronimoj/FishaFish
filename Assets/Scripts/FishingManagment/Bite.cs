using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    /// <summary>
    /// A reference to the instance with the correct settings
    /// </summary>
    public static Bite instance;

    /// <summary>
    /// The longest time it can take for a fish to bite
    /// </summary>
    [Tooltip("The longest time it can take for a fish to bite")]
    public float MaxFishTime;
    /// <summary>
    /// The shortest time it can take for a fish to bite
    /// </summary>
    [Tooltip("The shortest time it can take for a fish to bite")]
    public float MinFishTime;
    /// <summary>
    /// The randomised time that the fish will bite at.
    /// </summary>
    float biteTime;

    /// <summary>
    /// The time the player has to press the reel input within in order to begin the catch phase
    /// </summary>
    [Tooltip("The time the player has to press the reel input within in order to begin the catch phase")]
    public float ReelWindow;

    /// <summary>
    /// The length of time the player has been in the bite phase for
    /// </summary>
    float fishTimer;
    /// <summary>
    /// The time spent within the reeling window
    /// </summary>
    float reelTimer;

    //Booleans used to determine if the timers should be increasing
    bool isFishing;
    bool isReeling;

    public void Start()
    {
        //Make sure only one instance of this script exists
        //We do this for the same reason as the FishingManager
        if (instance == null)
            instance = this;
        else if (instance != null && instance != this)
            Destroy(this.gameObject);
    }

    public void Update()
    {
        if (isFishing)
        {
            fishTimer += Time.deltaTime;

            if (fishTimer >= biteTime)
                ReelBegin();
        }
        else if (isReeling)
        {
            reelTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isReeling = false;

                FishingManager.instance.NextPhase();
                return;
            }

            if (reelTimer >= ReelWindow)
                ReelFailed();
        }
    }

    public void StartPhase()
    {
        fishTimer = 0;
        reelTimer = 0;

        isFishing = true;
        biteTime = Random.Range(MinFishTime, MaxFishTime);
    }

    public void ReelBegin()
    {
        Debug.Log("A fish took the bait!");
        isFishing = false;
        isReeling = true;
    }

    public void ReelFailed()
    {
        Debug.Log("The fish got away...");
        isReeling = false;

        //Let the manager know you can begin fishing again
        FishingManager.instance.FinishFishing();
    }
}
