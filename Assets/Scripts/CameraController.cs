using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //El transform que deberá seguir

    [SerializeField] private Transform player;
    [SerializeField] private BoxCollider2D ground;
    
    [SerializeField] private float deadZone = 5f;

    Camera cam;
    float cameraHalfWidth;
    float minX;
    float maxX;

    float groundLeft;
    float groundRight;

    float cameraLimit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
        cameraHalfWidth = cam.orthographicSize * cam.aspect;

        groundLeft = ground.bounds.min.x;
        groundRight = ground.bounds.max.x;

        minX = groundLeft + cameraHalfWidth;
        maxX = groundRight - cameraHalfWidth;

    }

    // Update is called once per frame
    void Update()
    {
        float xDistance = player.position.x - cam.transform.position.x;
        float yDistance = player.position.y - cam.transform.position.y;
        float absDistance = Mathf.Abs(xDistance);

        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + yDistance,
            cam.transform.position.z);

        if (absDistance > deadZone)
        {
            float direction = Mathf.Sign(xDistance);
            float movement = xDistance - direction * deadZone;
            cam.transform.position = new Vector3(cam.transform.position.x + movement, 
                cam.transform.position.y, cam.transform.position.z);
        }

        cameraLimit = Mathf.Clamp(cam.transform.position.x, minX, maxX);
        cam.transform.position = new Vector3(cameraLimit, cam.transform.position.y,
            cam.transform.position.z);


    }
}
