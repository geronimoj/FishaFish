using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Determines the phases of the fishing game
/// </summary>
public class FishingManager : MonoBehaviour
{
    /// <summary>
    /// A reference to the instance with the correct settings
    /// </summary>
    [HideInInspector]
    public static FishingManager instance;

    #region Phases
    public enum fishingPhase
    {
        Approach,
        Bite,
        Catch
    }
    fishingPhase currentPhase;

    public fishingPhase CurrentPhase
    {
        get { return currentPhase; }
    }

    /// <summary>
    /// Add events that occur when the player starts fishing
    /// </summary>
    [InfoBox("Add events that occur when the player starts fishing")]
    public UnityEvent CastEvent;
    /// <summary>
    /// Add events that occur when the fish bites the lure
    /// </summary>
    [InfoBox("Add events that occur when the fish bites the lure")]
    public UnityEvent BiteEvent;
    /// <summary>
    /// Add events that occur when the fish escapes
    /// </summary>
    [InfoBox("Add events that occur when the fish escapes")]
    public UnityEvent EscapeEvent;
    /// <summary>
    /// Add events that occur when you enter the catch phase
    /// </summary>
    [InfoBox("Add events that occur when you enter the catch phase")]
    public UnityEvent BeginCatchEvent;
    /// <summary>
    /// Add events that occur when you successfully catch the fish
    /// </summary>
    [InfoBox("Add events that occur when you successfully catch the fish")]
    public UnityEvent FinishCatchEvent;
    #endregion

    /// <summary>
    /// Unlike the Bite script's isFishing, this boolean is just to avoid interactions between scripts while the player is fishing
    /// We don't want the player moving around or starting to fish a second time while they are fishing.
    /// </summary>
    [HideInInspector]
    public static bool fishing;

    /// <summary>
    /// The fish that will bite
    /// </summary>
    [HideInInspector]
    public Fish fish;

    /// <summary>
    /// The time it takes for the player to begin fishing again once they catch a fish
    /// This is to prevent the player from starting fishing with the same input that caught the fish
    /// </summary>
    private float fishBuffer = 0.1f;
    /// <summary>
    /// When this >= bufferTime, the player can begin fishing again
    /// </summary>
    private float bufferTime;
    /// <summary>
    /// Determines if the fishBuffer should be counting up
    /// </summary>
    private bool buffering;

    public void Start()
    {
        //Make sure only one instance of this script exists
        //FishingManager holds some settings for the minigame's design and more than manager may mess with a user's fishing settings
        if (instance == null)
            instance = this;
        else if (instance != null && instance != this)
            Destroy(this.gameObject);

        currentPhase = fishingPhase.Approach;
    }

    public void Update()
    {
        //Begins fishing when the player presses the space bar
        if (Input.GetKeyDown(KeyCode.Space) && !fishing && !buffering)
        {
            StartFishing();
        }
        if (buffering)
        {
            bufferTime += Time.deltaTime;

            if(bufferTime >= fishBuffer)
            {
                buffering = false;
                bufferTime = 0;
            }
        }
    }

    /// <summary>
    /// Checks any variables to the fishing scenario caused by the approach phase, then begins bite phase
    /// </summary>
    public void StartFishing()
    {
        //Prevent anything other than fishing
        fishing = true;
        fish = AvailableFishManager.instance.GetFish();

        NextPhase();
        Debug.Log("You cast the lure out!");
        CastEvent.Invoke();
    }

    public void NextPhase()
    {
        switch (currentPhase)
        {
            case fishingPhase.Approach:
                currentPhase = fishingPhase.Bite;
                Bite.instance.StartPhase();
                break;

            case fishingPhase.Bite:
                currentPhase = fishingPhase.Catch;
                BeginCatchEvent.Invoke();
                Catch.instance.BeginCatch();
                break;
        }
    }

    public void FinishFishing()
    {
        buffering = true;
        fishing = false;
        fish = null;
        currentPhase = fishingPhase.Approach;
    }
}
