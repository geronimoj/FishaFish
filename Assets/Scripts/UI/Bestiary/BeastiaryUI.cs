using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Fishing.UI
{
    public class BeastiaryUI : MonoBehaviour
    {
        [SerializeField] FishSetUI _fishSetUIPrefab = null;
        [SerializeField] Transform _fishSetParent = null;
        [SerializeField] GameObject[] _toggleAlt = null;

        bool _initialized = false;

        public UnityEvent onOpen = null;
        public UnityEvent onClose = null;

        private void Start()
        {   // Close UI when we start fishing
            FishingManager.instance.CastEvent.AddListener(Close);
            Close();
        }

        public void Toggle()
        {
            if (gameObject.activeSelf)
                Close();
            else
                Open();
        }
        /// <summary>
        /// Open the bestiary
        /// </summary>
        public void Open()
        {   // Don't allow opening if fishing
            if (FishingManager.fishing)
                return;

            gameObject.SetActive(true);

            Initialize();

            foreach (var obj in _toggleAlt)
                obj.SetActive(false);

            onOpen?.Invoke();
        }
        /// <summary>
        /// Close the bestiary
        /// </summary>
        public void Close()
        {
            gameObject.SetActive(false);

            foreach (var obj in _toggleAlt)
                obj.SetActive(true);

            onClose?.Invoke();
        }
        /// <summary>
        /// Initialize the bestiary UI 
        /// </summary>
        private void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            // Initialize the sets in UI
            foreach(var setData in AvailableFishManager.instance.FishSets)
            {
                var set = setData.set;
                var ui = Instantiate(_fishSetUIPrefab, _fishSetParent);
                ui.Initialize(set);
            }
        }
    }
}
