using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTween : MonoBehaviour
{
	[SerializeField] Gradient color;
	[SerializeField] float speed;

	//[SerializeField] float tweenTime = 1;

	float currentTime;

	TextMeshProUGUI text;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		//TweenToColor();
	}

	//void TweenToColor()
	//{
	//	var color = new Color(Random.Range(0f, 1), Random.Range(0f, 1), Random.Range(0f, 1), 1);
	//	//var color = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2), 1);
	//
	//	text.DOColor(color, tweenTime).OnComplete(TweenToColor);
	//}

	private void Update()
	{
		currentTime += Time.deltaTime * speed;

		if(currentTime > 1)
		{
			currentTime = 0;
		}

		text.color = color.Evaluate(currentTime);
	}
}
