using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new fish set", menuName = "FishSet")]
public class FishSet : ScriptableObject
{
    [SerializeField] string setName;
    public string SetName => setName;

    [SerializeField] Fish[] fishInSet;
    public Fish[] FishInSet => fishInSet;


    public bool FinishedSet
    {
        get
        {
            foreach (var f in FishInSet)
            {
                if (f.Caught == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}