using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fishing.UI
{
    public class BeastiaryUI : MonoBehaviour
    {
        [SerializeField] FishSetUI _fishSetUIPrefab = null;
        [SerializeField] FishSet[] _allFish = null;
        [SerializeField] Grid _fishSetParent = null;

        bool _initialized = false;

        public void Open()
        {
            gameObject.SetActive(true);

            Initialize();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            // Initialize the sets in UI
            foreach(var set in _allFish)
            {
                var ui = Instantiate(_fishSetUIPrefab, _fishSetParent.transform);
                ui.Initialize(set);
            }
        }
    }
}
