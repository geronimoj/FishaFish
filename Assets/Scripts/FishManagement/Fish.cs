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
