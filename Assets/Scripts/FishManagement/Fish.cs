using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new fish", menuName = "Fish")]
public class Fish : ScriptableObject
{
	[SerializeField] string fishName;
	public string FishName => fishName;

	[TextArea(3, 10)]
	[SerializeField] string fishDescription;
	public string FishDesciption => fishDescription;

	[SerializeField] Sprite fishIcon;
	public Sprite FishIcon => fishIcon;

	public bool Caught { get; set; }
}
