using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlots : MonoBehaviour
{
    private const int ColumnCount = 5;

    [SerializeField]
    private GameObject _slotsContainer = null;
    [SerializeField]
    private GameObject _slotPrefab = null;
    [SerializeField]
    private int _slots = 25;

    private List<InvButton> _buttons = new List<InvButton>();

    void Start()
    {
        if (_slotsContainer == null || _slotPrefab == null) return;

        for (int i = 0; i < _slots; i++)
		{
            var s = Instantiate(_slotPrefab, _slotsContainer.transform);
            s.name = $"Slot {i}";
            _buttons.Add(s.GetComponent<InvButton>());
            s.GetComponent<InvButton>().index = i;
   //         if(i != 0 && i % ColumnCount == 0)
   //{
   //             var a = s.GetComponent<InvButton>().navigation;
   //             //a.mode = Navigation.Mode.Explicit;
   //             Debug.LogError($"{_buttons[i - 1].gameObject.name}");
   //             a.selectOnLeft = _buttons[i - 1];

            //         }
        }
    }
}
