using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollBarTest : MonoBehaviour
{
	public GameObject slots;

	private GameObject lastSelected;
	private double scrollMoveFraction;

	public void OnValueChangeTest()
	{
		//Debug.Log($"{GetComponent<ScrollRect>().verticalNormalizedPosition}");
	}

	private void Start()
	{
		var child = slots.transform.childCount;
		var step1 = child - 25;
		var step2 = step1 / 5;
		var step3 = Mathf.CeilToInt(step2);

		scrollMoveFraction = 1d / step3;
		//scrollMoveFraction = 1d / Mathf.CeilToInt((child - 25) / 5);
		Debug.Log($"{child} -> {step1} -> {step2} -> {step3} -> {scrollMoveFraction}");
	}

	public void Update()
	{
		var current = EventSystem.current.currentSelectedGameObject;
		if (current != lastSelected)
		{
			Debug.Log($"Changed to {current.name}");
			lastSelected = current;
		}
	}
}
