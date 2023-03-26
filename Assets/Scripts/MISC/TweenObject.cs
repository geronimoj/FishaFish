using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenObject : MonoBehaviour
{
	[SerializeField] Vector3 startPos;
	[SerializeField] Vector3 startRot;
	[SerializeField] Vector3 startScale = Vector3.one;

	[SerializeField] float tweenTime = 1;

	[SerializeField] Ease tweenEase = Ease.Linear;

	Vector3 endPos;
	Vector3 endRot;
	Vector3 endScale;

	private void Awake()
	{
		endPos = transform.localPosition;
		endRot = transform.localEulerAngles;
		endScale = transform.localScale;

		transform.localPosition = startPos;
		transform.localEulerAngles = startRot;
		transform.localScale = startScale;
	}

	[Button]
	public void TweenIn()
	{
		transform.DOLocalMove(endPos, tweenTime).SetEase(tweenEase);
		transform.DORotate(endRot, tweenTime).SetEase(tweenEase);
		transform.DOScale(endScale, tweenTime).SetEase(tweenEase);
	}
	[Button]
	public void TweenOut()
	{
		transform.DOLocalMove(startPos, tweenTime).SetEase(tweenEase);
		transform.DORotate(startRot, tweenTime).SetEase(tweenEase);
		transform.DOScale(startScale, tweenTime).SetEase(tweenEase);
	}
}
