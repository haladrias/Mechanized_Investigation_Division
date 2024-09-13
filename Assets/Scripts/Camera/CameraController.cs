
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float camSpeed = 20f;
    [SerializeField] private float camBorderThickness = 20f;
    [SerializeField] private Vector2 camLimit;

    [SerializeField] private float scrollSpeed = 20f;
    [SerializeField] private float minY = 3f;
    [SerializeField] private float maxY = 20;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 position = transform.position;
        if (Input.GetKey("w") || Input.GetKey("up") || Input.mousePosition.y >= Screen.height - camBorderThickness)
        {

            position.z += camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.GetKey("down") || Input.mousePosition.y <= camBorderThickness)
        {
            position.z -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.GetKey("left") || Input.mousePosition.x <=  camBorderThickness)
        {
            position.x -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.GetKey("right") || Input.mousePosition.x >= Screen.width - camBorderThickness )
        {
            position.x += camSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        position.y -= scroll * scrollSpeed * 50f * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -camLimit.x, camLimit.x);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        position.z = Mathf.Clamp(position.z, -camLimit.y, camLimit.y);


        transform.position = position;
    }
}
