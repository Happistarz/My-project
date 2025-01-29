using System;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    [SerializeField] private Canvas parentCanvas;

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale == 0) return; // The game is paused, don't update the cursor

        try
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                parentCanvas.worldCamera,
                out var pos
            );

            transform.position = parentCanvas.transform.TransformPoint(pos);
        } catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}