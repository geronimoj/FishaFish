using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    /// <summary>
    /// A reference to the instance with the correct settings
    /// </summary>
    public static Catch instance;

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


    public void Start()
    {
        //Make sure only one instance of this script exists
        //We do this for the same reason as the FishingManager
        if (instance == null)
            instance = this;
        else if (instance != null && instance != this)
            Destroy(this.gameObject);
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
        Debug.Log("You dun did it");
        FishingManager.instance.FinishFishing();
    }
}
