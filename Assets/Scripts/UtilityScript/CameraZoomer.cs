using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour {

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.001f;        // The rate of change of the orthographic size in orthographic mode.

    float cameraDistanceMax = 20f;
    float cameraDistanceMin = 5f;
    float cameraDistance = 10f;
    float scrollSpeed = 3;

    Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ... change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
            camera.orthographicSize = cameraDistance;
        }
    }
}
