using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing.UI
{
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

            catchBar.localPosition = new Vector3(0, catchManager.RangePos);
            fishBar.localPosition = new Vector3(0, catchManager.RangePos);

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