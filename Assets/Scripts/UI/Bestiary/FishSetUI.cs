using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fishing.UI
{
    /// <summary>
    /// Manages UI for a fish set
    /// </summary>
    public class FishSetUI : MonoBehaviour
    {
        [SerializeField] FishBeastUI _fishUIPrefab = null;
        [SerializeField] Transform _fishParent = null;
        [SerializeField] TMP_Text _setName = null;
        [SerializeField] GameObject _setFinishIcon = null;

        FishSet _set = null;
        bool _completedSet = false;

        public void Initialize(FishSet set)
        {
            _set = set;
            _completedSet = set.FinishedSet;

            gameObject.SetActive(false);
            _setFinishIcon.SetActive(false);
            // Initialize Fish
            foreach (var fish in set.FishInSet)
            {
                var ui = Instantiate(_fishUIPrefab, _fishParent);
                ui.Initialize(fish);

                fish.OnCatch -= OnCatchFish;
                fish.OnCatch += OnCatchFish;
            }

            if (_setName)
                _setName.text = set ? set.SetName : null;
            // Make sure UI is correct
            if (_completedSet)
                OnSetComplete();
        }

        private void OnEnable()
        {
            if (_set && _set.FinishedSet)
                OnSetComplete();
        }

        private void OnSetComplete()
        {
            _setFinishIcon.SetActive(true);
        }

        private void OnCatchFish()
        {   
            if (_set.FinishedSet)
                OnSetComplete();

            gameObject.SetActive(true);
        }
    }
}