using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnTest : MonoBehaviour
{
	public Button btnSave;
	public Button btnLoad;

	public void Start()
	{
		// get the Navigation data
		Navigation navigation = btnLoad.navigation;

		// switch mode to Explicit to allow for custom assigned behavior
		navigation.mode = Navigation.Mode.Explicit;

		// highlight the Save button if the up arrow key is pressed
		navigation.selectOnUp = btnSave;

		// reassign the struct data to the button
		btnLoad.navigation = navigation;
	}
}
