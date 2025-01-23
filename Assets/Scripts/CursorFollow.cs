using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    [SerializeField] private Canvas parentCanvas;

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale == 0) return; // The game is paused, don't update the cursor

        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.GetComponent<RectTransform>(),
            Input.mousePosition,
            parentCanvas.worldCamera,
            out pos
        );

        transform.position = parentCanvas.transform.TransformPoint(pos);
    }
}