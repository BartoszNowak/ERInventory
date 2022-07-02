using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollBarTest : MonoBehaviour
{
	private ScrollRect scrollRect;
	public GameObject slots;

	private int lastSelected;
	private float scrollMoveFraction;

	public void OnValueChangeTest()
	{
		//Debug.Log($"{GetComponent<ScrollRect>().verticalNormalizedPosition}");
	}

	private void Start()
	{
		scrollRect = GetComponent<ScrollRect>();
		var child = slots.transform.childCount;
		var step1 = Mathf.Max(child - 25, 0);
		var step2 = step1 / 5;
		var rows = Mathf.CeilToInt(step2);

		scrollMoveFraction = 1f / rows;
		//scrollMoveFraction = 1d / Mathf.CeilToInt((child - 25) / 5);
		Debug.Log($"{child} -> {step1} -> {step2} -> {rows} -> {scrollMoveFraction}");
	}

	public void Update()
	{
		return;
		var current = EventSystem.current.currentSelectedGameObject;
		if (current == null) return;

		int i = current.GetComponent<InvButton>().index;
		if (i != lastSelected)
		{
			Debug.Log($"Changed to {i}");

			if(i < lastSelected)
			{
				scrollRect.verticalNormalizedPosition += scrollMoveFraction;
			}
			else
			{
				scrollRect.verticalNormalizedPosition -= scrollMoveFraction;
			}

			lastSelected = i;
		}
	}
}
