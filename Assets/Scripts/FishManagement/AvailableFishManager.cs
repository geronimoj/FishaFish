using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvailableFishManager : MonoBehaviour
{
	[System.Serializable]
	public class FishSetData
	{
		public FishSet set;
		public GameObject furnitureToEnable;
	}

	[SerializeField] FishSetData[] fishSetData;

	List<Fish> allFish = new List<Fish>();

	List<Fish> caughtFish = new List<Fish>();

	public static AvailableFishManager instance;

	/// <summary>
	/// Called when the game finishes
	/// </summary>
	public UnityEvent OnGameFinished;

	List<FishSet> GetActiveFishSets()
	{
		int maxFishSets = 5;
		List<FishSet> sets = new List<FishSet>();

		foreach (var s in fishSetData)
		{
			if (s.set.FinishedSet == false)
			{
				sets.Add(s.set);

				if (sets.Count >= maxFishSets)
				{
					break;
				}
			}
		}

		return sets;
	}

	private void Awake()
	{
		if (instance)
			return;

		instance = this;

		foreach (var s in fishSetData)
		{
			foreach (var f in s.set.FishInSet)
			{
				allFish.Add(f);
			}
		}

		LoadCaughtFish();
	}

	void LoadCaughtFish()
	{
		foreach (var f in allFish)
		{
			if (PlayerPrefs.GetInt(f.FishName, 0) == 1)
			{
				CatchFish(f);
			}
		}
	}

	/// <summary>
	/// DYLAN CALL THIS FUNCTION WHEN YOU CATCH A FISH
	/// </summary>
	/// <param name="f"></param>
	void OnFishCaught(Fish f)
	{
		PlayerPrefs.SetInt(f.FishName, 1);
		CatchFish(f);

		if (GetActiveFishSets().Count == 0)
		{
			OnGameFinished?.Invoke();
		}
	}

	void CatchFish(Fish f)
	{
		f.Caught = true;
		caughtFish.Add(f);
	}

	/// <summary>
	/// DYLAN USE THIS FUNCTION TO GET A FISH TO CATCH
	/// </summary>
	/// <returns></returns>
	public Fish GetFish()
	{
		var fishSets = GetActiveFishSets();

		if (fishSets.Count == 0)
		{
			Debug.LogError("The game should be completed, why are you fishing still");

			return null;
		}

		var s = fishSets[Random.Range(0, fishSets.Count)];

		return s.FishInSet[Random.Range(0, s.FishInSet.Length)];
	}

#if UNITY_EDITOR
	//TODO: Make function to load all fish in editor to stop us from dragging shizzle
#endif
}