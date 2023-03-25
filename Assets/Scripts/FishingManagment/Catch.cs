using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    /// <summary>
    /// A reference to the instance with the correct settings
    /// </summary>
    [HideInInspector]
    public static Catch instance;

    #region Catch Variables
    /// <summary>
    /// What is the minigame you want to play after you've begun catch phase
    /// </summary>
    public enum catchType
    {
        /// <summary>
        /// Catch phase ends automatically and the player catches the fish
        /// PURELY FOR TESTING FISH CATCHING
        /// </summary>
        AutoCatch,
        /// <summary>
        /// The player has to keep the fish within a manipulatable range
        /// </summary>
        RangeCatch
    };
    /// <summary>
    /// This variable creates the drop-down selection in Unity for easy use in-editor
    /// </summary>
    [Tooltip("The minigame that begins once the player reaches catch phase")]
    public catchType CatchType;
    /// <summary>
    /// How close the player is to catching the fish
    /// </summary>
    [HideInInspector]
    public float catchProgress;
    /// <summary>
    /// The amount of progress the player must reach before the fish is caught
    /// FOR RANGE CATCHING THIS IS THE NUMBER OF SECONDS YOU NEED TO SPEND CATCHING IT
    /// </summary>
    [Tooltip("The amount of progress the player must reach before the fish is caught")]
    public float ProgressRequired;
    #endregion

    #region Range Catching Variables
    /// <summary>
    /// The highest value the fishRange or catchRange can get before being stopped
    /// </summary>
    [Tooltip("The highest value the fishRange or catchRange can get before being stopped")]
    public float CatchBarMax;

    /// <summary>
    /// The value range of which you must keep the fishes value within in order to gain catch progress
    /// </summary>
    [Tooltip("The value range of which you must keep the fishes value within in order to gain catch progress")]
    public float CatchRange;
    /// <summary>
    /// How fast the catch range moves in the catch bar
    /// </summary>
    [Tooltip("How fast the catch range moves in the catch bar")]
    [Range(1,20)]
    public float RangeSpeed;
    /// <summary>
    /// the position of the centre of the player's catch range
    /// </summary>
    float rangePosition;
    /// <summary>
    /// The position of the centre of the fish
    /// </summary>
    float fishPosition;

    public float FishPos => fishPosition;
    public float RangePos => rangePosition;

    /// <summary>
    /// The time before the fish decides their movement direction
    /// </summary>
    [Tooltip("The time before the fish decides their movement direction")]
    public float DecisionTime = 1;
    /// <summary>
    /// Once this equals DecisionTime, the fish decides the next direction
    /// </summary>
    float decisionTimer;
    /// <summary>
    /// Is the fish swimming upwards?
    /// </summary>
    bool swimUpwards;
    #endregion

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
        if (CatchType == catchType.RangeCatch && FishingManager.instance.CurrentPhase == FishingManager.fishingPhase.Catch)
        {
            //Move the catch range upwards if it hasn't hit the top
            if (Input.GetKey(KeyCode.Space))
            {
                if (rangePosition + (CatchRange / 2) < CatchBarMax)
                    rangePosition += (Time.deltaTime * RangeSpeed);
            }
            //Move the range downwards if it hasn't hit the bottom
            else if (rangePosition - (CatchRange / 2) > 0)
            {
                rangePosition -= (Time.deltaTime * RangeSpeed);
            }

            //Move the fish up or down
            if(swimUpwards && fishPosition + (FishingManager.instance.fish.FishRange / 2) < CatchBarMax)
            {
                fishPosition += (Time.deltaTime * FishingManager.instance.fish.FishSpeed);
            }
            else if(!swimUpwards && fishPosition - (FishingManager.instance.fish.FishRange / 2) > CatchBarMax)
            {
                fishPosition -= (Time.deltaTime * FishingManager.instance.fish.FishSpeed);
            }

            //If the catch range overlaps with the fish range, add progress
            //First check if the top end of the fish range overlaps with the catch range, then do the same for the bottom of the fish range
            if (rangePosition - (CatchRange / 2) < fishPosition + (FishingManager.instance.fish.FishRange / 2) &&
                rangePosition + (CatchRange / 2) > fishPosition + (FishingManager.instance.fish.FishRange) ||
                rangePosition - (CatchRange / 2) < fishPosition - (FishingManager.instance.fish.FishRange / 2) &&
                rangePosition + (CatchRange / 2) > fishPosition - (FishingManager.instance.fish.FishRange))
                //Increase the progress meter as there is overlap
                catchProgress += Time.deltaTime;
            else
                //Decrease the progress meter as there is no overlap
                catchProgress -= Time.deltaTime;

            //Check if the progress required has been met
            if(catchProgress >= ProgressRequired)
            {
                SucceedCatch();
                return;
            }

            //After enough time has passed, make the fish randomly select another direction to move in
            if (decisionTimer >= DecisionTime)
                DecideDirection();
            
            decisionTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Catch phase begins when this function is called.
    /// Determines how the phase plays based on the settings
    /// </summary>
    public void BeginCatch()
    {
        switch (CatchType)
        {
            case catchType.AutoCatch:
                SucceedCatch();
                break;

            case catchType.RangeCatch:
                rangePosition = CatchRange / 2;
                fishPosition = CatchBarMax / 2;
                
                //Make the UI appear
                break;
        }
    }

    void SucceedCatch()
    {
        Debug.Log("You caught a " + FishingManager.instance.fish.FishName + "!");
        FishingManager.instance.FinishCatchEvent.Invoke();
        catchProgress = 0;

        AvailableFishManager.instance.OnFishCaught(FishingManager.instance.fish);
        FishingManager.instance.FinishFishing();
    }

    /// <summary>
    /// Randomly selects which direction the fish will start moving in
    /// </summary>
    void DecideDirection()
    {
        int decision = Random.Range(0, 2);
        if (decision == 0)
            swimUpwards = false;
        else
            swimUpwards = true;

        decisionTimer = 0;
    }
}
