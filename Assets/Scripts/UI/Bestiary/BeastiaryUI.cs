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
        [SerializeField] FishSet[] _allFish = null;
        [SerializeField] Transform _fishSetParent = null;

        bool _initialized = false;

        public UnityEvent onOpen = null;
        public UnityEvent onClose = null;

        private void Start()
        {
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
        {
            gameObject.SetActive(true);

            Initialize();

            onOpen?.Invoke();
        }
        /// <summary>
        /// Close the bestiary
        /// </summary>
        public void Close()
        {
            gameObject.SetActive(false);
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
            foreach(var set in _allFish)
            {
                var ui = Instantiate(_fishSetUIPrefab, _fishSetParent);
                ui.Initialize(set);
            }
        }
    }
}
