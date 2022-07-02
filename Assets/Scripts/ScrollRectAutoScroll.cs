using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectAutoScroll : MonoBehaviour
{
    public float scrollSpeed = 10f;
    private bool mouseOver = false;

    private List<Selectable> m_Selectables = new List<Selectable>();
    private ScrollRect m_ScrollRect;

    private Vector2 m_NextScrollPosition = Vector2.up;

    private int rows;
    private float scrollMoveFraction;

    private int lastSelected;
    //private int lastRow;

    void OnEnable()
    {
        if (m_ScrollRect)
        {
            m_ScrollRect.content.GetComponentsInChildren(m_Selectables);
        }
    }
    void Awake()
    {
        m_ScrollRect = GetComponent<ScrollRect>();
    }
    void Start()
    {
        if (m_ScrollRect)
        {
            m_ScrollRect.content.GetComponentsInChildren(m_Selectables);
            if(m_Selectables.Count > 0)
			{
                EventSystem.current.firstSelectedGameObject = m_Selectables[0].gameObject;
			}
        }
        Setup();
        //ScrollToSelected(true);
    }
    void Update()
    {
        // Scroll via input.
        //InputScroll();
        TriggerScroll();
        if (!mouseOver)
        {
            // Lerp scrolling code.
            //m_ScrollRect.normalizedPosition = Vector2.Lerp(m_ScrollRect.normalizedPosition, m_NextScrollPosition, scrollSpeed * Time.unscaledDeltaTime);
        }
        else
        {
            m_NextScrollPosition = m_ScrollRect.normalizedPosition;
        }
    }
    void InputScroll()
    {
        if (m_Selectables.Count > 0)
        {
            //remove the rePlayer getaxis calls is you aren't using Rewired
            //if it still doesn't work, check your input manager settings's axes and make sure they are defined properly
            //if you're using the new input system, this is also probably where you should replace the calls to the old one
            if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f || Input.GetButtonDown("Horizontal")
                || Input.GetButtonDown("Vertical") || Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                ScrollToSelected(false);
            }
        }
    }

    private void TriggerScroll()
	{
        Selectable selectedElement = EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;
        int selectedIndex = 0;
        if (selectedElement)
        {
            selectedIndex = m_Selectables.IndexOf(selectedElement);
        }
        if (selectedIndex != lastSelected)
        {
            var lastRow = Mathf.CeilToInt(lastSelected / 5);
            var selectedRow = Mathf.CeilToInt(selectedIndex / 5);

            if (selectedRow == lastRow) return;

            int changeSign = selectedRow > lastRow ? -1 : 1;

            var newValue = m_ScrollRect.verticalNormalizedPosition + (scrollMoveFraction * changeSign);
            m_ScrollRect.verticalNormalizedPosition = Mathf.Clamp01(newValue);

            Debug.Log($"Changed from [{lastSelected} row {lastRow}] to [{selectedIndex} row {selectedRow}] position {m_ScrollRect.verticalNormalizedPosition}");

            lastSelected = selectedIndex;
        }
    }

    void ScrollToSelected(bool quickScroll)
    {
        int selectedIndex = -1;
        Selectable selectedElement = EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;

        int selectedRow = -1;

        if (selectedElement)
        {
            selectedIndex = m_Selectables.IndexOf(selectedElement);
            selectedRow = Mathf.CeilToInt(selectedIndex / 5);
        }
        if (selectedRow > -1)
        {

            //int v = selectedRow > lastRow ? 1 : -1;
            //var clmp = Mathf.Clamp(m_ScrollRect.verticalNormalizedPosition + (scrollMoveFraction * v), 0f, 1f);

            //if (quickScroll)
            //{
            //    m_ScrollRect.normalizedPosition = new Vector2(0, clmp);
            //    m_NextScrollPosition = m_ScrollRect.normalizedPosition;
            //}
            //else
            //{
            //    m_NextScrollPosition = new Vector2(0, clmp);
            //    Debug.Log($"{m_ScrollRect.verticalNormalizedPosition}");
            //}

            //lastRow = selectedRow;

            // first
            //if (quickScroll)
            //{
            //    m_ScrollRect.normalizedPosition = new Vector2(0, 1 - (selectedRow / ((float)rows)));
            //    m_NextScrollPosition = m_ScrollRect.normalizedPosition;
            //}
            //else
            //{
            //    m_NextScrollPosition = new Vector2(0, 1 - (selectedRow / ((float)rows)));
            //    Debug.Log($"{m_ScrollRect.verticalNormalizedPosition}");
            //}

            //original
            //if (quickScroll)
            //{
            //    m_ScrollRect.normalizedPosition = new Vector2(0, 1 - (selectedIndex / ((float)m_Selectables.Count - 1)));
            //    m_NextScrollPosition = m_ScrollRect.normalizedPosition;
            //}
            //else
            //{
            //    m_NextScrollPosition = new Vector2(0, 1 - (selectedIndex / ((float)m_Selectables.Count - 1)));
            //}
        }
    }

    private void Setup()
	{
        var child = m_Selectables.Count;
        var step1 = Mathf.Max(child - 25, 0);
        var step2 = step1 / 5;
        rows = Mathf.CeilToInt(step2);

        scrollMoveFraction = 1f / rows;
        Debug.Log($"{child} -> {step1} -> {step2} -> {rows} -> {scrollMoveFraction}");
    }
}