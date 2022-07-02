using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/New Item", order = 0)]
public class Item : ScriptableObject
{
	public string Name = "New Item";
	public Sprite Icon;
	public int Amount = 1;
	public ItemCategory Category;
}