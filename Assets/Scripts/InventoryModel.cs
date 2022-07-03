using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : MonoBehaviour
{
	[SerializeField]
	private List<Item> _items = new List<Item>();

	public event Action OnChange;
	public List<Item> Items => _items;

	public void Add(Item item)
	{
		_items.Add(item);
		OnChange?.Invoke();
	}

	public void Remove(Item item)
	{
		_items.Remove(item);
		OnChange?.Invoke();
	}

	public bool Contains(Item item)
	{
		return _items.Contains(item);
	}

	private void Awake()
	{
		_items[0].Amount = 2;
	}
}