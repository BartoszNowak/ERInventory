using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
	[SerializeField]
    private Image _icon;
	[SerializeField]
	private TMP_Text _amount;

	public void Setup(Sprite icon, int amount)
	{
		_icon.sprite = icon;
		_amount.text = amount.ToString();
	}
}
