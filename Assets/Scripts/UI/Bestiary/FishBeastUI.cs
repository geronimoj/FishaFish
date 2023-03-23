using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fishing.UI
{
    /// <summary>
    /// Common, reusable UI item for Fish
    /// </summary>
    public class FishBeastUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _nameText = null;
        [SerializeField] TMP_Text _descriptionText = null;
        [SerializeField] Image _fishIcon = null;

        Fish _fish = null;
        /// <summary>
        /// Initialize the UI with a specific fish
        /// </summary>
        /// <param name="fish"></param>
        public void Initialize(Fish fish)
        {
            //Unhook if already hooked onto another fish
            if (_fish)
                _fish.OnCatch -= OnFishCaught;

            if (_nameText)
                _nameText.text = fish ? fish.FishName : null;

            if (_descriptionText)
                _descriptionText.text = fish ? fish.FishDesciption : null;

            if (_fishIcon)
                _fishIcon.sprite = fish ? fish.FishIcon : null;

            _fish = fish;
            _fish.OnCatch += OnFishCaught;
        }

        private void OnDestroy()
        {
            if (_fish)
                _fish.OnCatch -= OnFishCaught;
        }

        private void OnFishCaught()
        {
            // Set UI to un hidden
        }
    }
}