using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing.UI
{
    [DefaultExecutionOrder(1)]
    public class FishingMinigameUI : MonoBehaviour
    {
        public static FishingMinigameUI Instance = null;

        [SerializeField] RectTransform catchBar = null;
        [SerializeField] RectTransform fishBar = null;

        [SerializeField] Slider progressSlider = null;

        private void Awake()
        {
            Instance = this;
            FishingManager instance = FishingManager.instance;

            instance.BeginCatchEvent.AddListener(Show);
            instance.FinishCatchEvent.AddListener(OnSuccess);
            instance.EscapeEvent.AddListener(OnFail);

            Close();
        }
        /// <summary>
        /// Make sure UI is good for fish catch
        /// </summary>
        /// <param name="fishToCatch"></param>
        public void Initialize(Fish fishToCatch)
        {
        }

        private void LateUpdate()
        {
            RefreshUI();
        }
        /// <summary>
        /// Update the UI
        /// </summary>
        private void RefreshUI()
        {
            Catch catchManager = Catch.instance;

            float progress = catchManager.catchProgress / catchManager.ProgressRequired;

            catchBar.localPosition = new Vector3(catchManager.RangePos, 0);
            catchBar.sizeDelta = new Vector2(catchManager.CatchRange, fishBar.sizeDelta.y);

            fishBar.localPosition = new Vector3(catchManager.FishPos, 0);
            fishBar.sizeDelta = new Vector2(FishingManager.instance.fish.FishRange, fishBar.sizeDelta.y);

            progressSlider.value = progress;
        }
        /// <summary>
        /// Successfully caught the fish
        /// </summary>
        private void OnSuccess()
        {
            Close();
        }
        /// <summary>
        /// Failed
        /// </summary>
        private void OnFail()
        {
            Close();
        }

        private void Show() => gameObject.SetActive(true);
        private void Close() => gameObject.SetActive(false);
    }
}