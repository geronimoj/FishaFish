using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Environment;
using Sirenix.OdinInspector;

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

	/// <summary>
	/// Key used for saving how many fish have been caught
	/// </summary>
	public const string FISH_CAUGH_KEY = "F";
	/// <summary>
	/// The number of fish that have been caught.
	/// </summary>
	private static int _caughtFish = 0;
	/// <summary>
	/// The number of fish that have been caught.
	/// </summary>
	public static int CaughtFigh => _caughtFish;

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
		//Load fish caught and use to calculate environment unlocks
		_caughtFish = PlayerPrefs.GetInt(FISH_CAUGH_KEY, 0);
		EnvironmentManager.LoadAlreadyUnlocked(_caughtFish);
	}

	/// <summary>
	/// DYLAN CALL THIS FUNCTION WHEN YOU CATCH A FISH
	/// </summary>
	/// <param name="f"></param>
	public void OnFishCaught(Fish f)
	{
		_caughtFish++;
		PlayerPrefs.SetInt(FISH_CAUGH_KEY, _caughtFish);
		PlayerPrefs.SetInt(f.FishName, 1);
		CatchFish(f);
		EnvironmentManager.OnCatchFish(_caughtFish);

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
    [Sirenix.OdinInspector.Button]
	private void NukeMySave()
	{
		PlayerPrefs.DeleteAll();

		var allFish = UnityEditor.AssetDatabase.FindAssets("t:Fish");

		foreach(var guid in allFish)
        {
			string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);

			Fish fish = UnityEditor.AssetDatabase.LoadAssetAtPath<Fish>(path);

			if (fish)
				fish.Caught = false;
        }
	}
#endif
}