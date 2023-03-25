using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fishing.UI
{
    /// <summary>
    /// Manages the Fishing Bite UI
    /// </summary>
    public class FishingBiteUI : MonoBehaviour
    {
        private void Start()
        {
            FishingManager.instance.BiteEvent.AddListener(Show);
            FishingManager.instance.EscapeEvent.AddListener(Close);
            FishingManager.instance.BeginCatchEvent.AddListener(Close);

            Close();
        }

        public void Show() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
    }
}