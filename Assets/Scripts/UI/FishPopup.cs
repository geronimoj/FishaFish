using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FishPopup : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI fishTitle;
	[SerializeField] string prefix = "You caught: ";

	[SerializeField] TextMeshProUGUI fishDescription;

	[Space]

	[SerializeField] Image fishImage;

	[Space]

	[SerializeField] Button closeBtn;

	public UnityEvent OnOpen;
	public UnityEvent OnClose;

    TweenObject o;

	private void Awake()
	{
		o = GetComponent<TweenObject>();

		closeBtn.onClick.AddListener(Close);
	}
	private void Start()
	{
		FishingManager.instance.FinishCatchEventWfish.AddListener(OnFishCaught);
	}

	public void OnFishCaught(Fish f)
	{
		o.TweenIn();
		fishTitle.SetText($"{prefix}{f.FishName}");

		fishImage.sprite = f.FishIcon;

		fishDescription.SetText(f.FishDesciption);

		FishingManager.canFish = false;

		OnOpen?.Invoke();
	}

	void Close()
	{
		o.TweenOut();
		OnClose?.Invoke();

		FishingManager.canFish = true;
	}
}
