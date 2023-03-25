using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new fish", menuName = "Fish")]
public class Fish : ScriptableObject
{
    [SerializeField] string fishName;
    public string FishName => fishName;

    [TextArea(3, 10)]
    [SerializeField] string fishDescription;
    public string FishDesciption => fishDescription;

    [SerializeField] Sprite fishIcon;
    public Sprite FishIcon => fishIcon;

    private bool caught = false;

    /// <summary>
    /// The longest time it can take for a fish to bite
    /// </summary>
    [Tooltip("The longest time it can take for a fish to bite")]
    public float MaxFishTime = 2;
    /// <summary>
    /// The shortest time it can take for a fish to bite
    /// </summary>
    [Tooltip("The shortest time it can take for a fish to bite")]
    public float MinFishTime = 2;

    /// <summary>
    /// The space that the fish occupies in the catch bar
    /// </summary>
    [Tooltip("The space that the fish occupies in the catch bar")]
    public float FishRange = 5;
    /// <summary>
    /// How fast the fish moves in comparison to the catch range
    /// </summary>
    [Tooltip("How fast the fish moves in comparison to the catch range")]
    [Range(1, 10)]
    public float FishSpeed;

    public bool Caught
    {
        get => caught;
        set
        {
            caught = value;

            if (value)
                onCatch?.Invoke();
        }
    }

    Action onCatch = null;

    public event Action OnCatch
    {
        add
        {
            if (caught)
            {
                value?.Invoke();
                return;
            }

            onCatch += value;
        }
        remove
        {
            onCatch -= value;
        }
    }
}
