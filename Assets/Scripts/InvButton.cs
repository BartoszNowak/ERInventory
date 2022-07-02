using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvButton : Button
{
	public int index;

	//public Button button;

	//protected void Start()
	//{
	//	button = GetComponent<Button>();

	//}



	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		//Debug.Log($"Now {gameObject.name}");
	}
}
