using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Environment
{
    /// <summary>
    /// Manages cosmetic unlocks
    /// </summary>
    public class EnvironmentManager : MonoBehaviour
    {
        public struct UnlockInfo
        {
            public int _fishCaughtToUnlock;
            public PlayableDirector _unlockAnimation;
        }
        /// <summary>
        /// The unlocks
        /// </summary>
        [SerializeField] List<UnlockInfo> _cosmeticUnlockInfo = null;
        /// <summary>
        /// Queue used to track unlocks left
        /// </summary>
        private static Queue<UnlockInfo> _unlockQueue = null;
        /// <summary>
        /// Key used for saving how many fish have been caught
        /// </summary>
        public const string FISH_CAUGH_KEY = "F";
        /// <summary>
        /// Are we currently loading
        /// </summary>
        private static bool _isLoading = false;

        private void Awake()
        {
            // Sort so its ordered by unlock
            _cosmeticUnlockInfo.RemoveAll((unlock) => !unlock._unlockAnimation);
            _cosmeticUnlockInfo.Sort(UnlockSort);
            // Put in queue because unlocks are ordered
            _unlockQueue ??= new Queue<UnlockInfo>(_cosmeticUnlockInfo);

            static int UnlockSort(UnlockInfo a, UnlockInfo b)
            {   // Sort via unlock time
                return a._fishCaughtToUnlock.CompareTo(b._fishCaughtToUnlock);
            }

            // Lock all unlocks
            foreach(var info in _cosmeticUnlockInfo)
            {
                var d = info._unlockAnimation;
                // Pause and hide the unlock
                d.Pause();
                d.gameObject.SetActive(false);
            }

            LoadSavedata();
        }
        /// <summary>
        /// Load any saved data
        /// </summary>
        private static void LoadSavedata()
        {
            _isLoading = true;
            // Load the caught fish. We flag as loading to ensure the unlock sequences are instant
            int caught = PlayerPrefs.GetInt(FISH_CAUGH_KEY, 0);
            OnCatchFish(caught);

            _isLoading = false;
        }
        /// <summary>
        /// Invoke when you catch a fish
        /// </summary>
        /// <param name="fishCaught">The total number of fish that have been caught over the lifetime of the game</param>
        public static void OnCatchFish(int fishCaught)
        {   // Unlock everything that should be unlocked
            while (true)
            {
                var info = _unlockQueue.Peek();

                if (fishCaught >= info._fishCaughtToUnlock)
                {   //Already unlocked so dequeue
                    _unlockQueue.Dequeue();
                    PlayUnlockSequence(info._unlockAnimation, _isLoading);
                }
                else
                    break;
            }
        }
        /// <summary>
        /// Plays the unlock sequence for a cosmetic
        /// </summary>
        /// <param name="directior"></param>
        /// <param name="instant"></param>
        private static void PlayUnlockSequence(PlayableDirector directior, bool instant = false)
        {
            directior.gameObject.SetActive(true);
            // If finish instantly, just set director to last frame
            if (instant && directior.playableAsset)
                directior.time = directior.playableAsset.duration;

            directior.Play();
        }
    }
}