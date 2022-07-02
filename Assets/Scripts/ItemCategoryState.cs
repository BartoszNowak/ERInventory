using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Category State", menuName = "Inventory/New Item Category", order = 0)]
public class ItemCategoryState : ScriptableObject
{
	public ItemCategoryState Previous;
	public ItemCategoryState Next;
	public ItemCategory Category;
	public Sprite CategoryIcon;
	public string CategoryName;
}
