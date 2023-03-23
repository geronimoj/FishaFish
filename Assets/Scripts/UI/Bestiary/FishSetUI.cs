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
        [SerializeField] LayoutGroup _fishParent = null;
        [SerializeField] TMP_Text _setName = null;

        FishSet _set = null;
        bool _completedSet = false;

        public void Initialize(FishSet set)
        {   // Initialize Fish
            foreach (var fish in set.FishInSet)
            {
                var ui = Instantiate(_fishUIPrefab, _fishParent.transform);
                ui.Initialize(fish);
            }

            if (_setName)
                _setName.text = set ? set.SetName : null;

            _set = set;
            _completedSet = set.FinishedSet;
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
        }
    }
}