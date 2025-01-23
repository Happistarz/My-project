using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private     Transform playerBody;
    [SerializeField] private new Camera    camera;

    // LateUpdate is called once per frame after all Update functions have been called
    private void LateUpdate()
    {
        if (Time.timeScale == 0) return; // The game is paused, don't update the camera

        var mouseScreenPos = Input.mousePosition;

        var ray = camera.ScreenPointToRay(mouseScreenPos);
        if (!Physics.Raycast(ray, out var hit)) return;

        var dir = hit.point - playerBody.position;
        dir.y = 0;

        playerBody.rotation = Quaternion.LookRotation(dir);
    }
}