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
    private float catchProgress;
    /// <summary>
    /// The amount of progress the player must reach before the fish is caught
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
        if (CatchType == catchType.RangeCatch)
        {

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
                break;
        }
    }

    void SucceedCatch()
    {
        Debug.Log("You caught a " + FishingManager.instance.fish.FishName + "!");
        FishingManager.instance.FinishCatchEvent.Invoke();
        AvailableFishManager.instance.OnFishCaught(FishingManager.instance.fish);
        FishingManager.instance.FinishFishing();
    }
}
