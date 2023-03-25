using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fishing.UI
{
    public class FishingMinigameUI : MonoBehaviour
    {
        public void Initialize(Fish fishToCatch)
        {
            Show();
        }

        private void Update()
        {
            RefreshUI();
        }

        private void RefreshUI()
        {

        }

        public void Show() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
    }
}