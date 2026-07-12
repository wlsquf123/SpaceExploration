using System.Threading;
using UnityEngine;

public class SpaceShipCamera : MonoBehaviour
{
    public Transform Camera;
    public float mouseSensitivity = 2.0f; // °¨µµ

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float MoX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float MoY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            transform.Rotate(Vector3.up * MoX);
            Camera.transform.Rotate(Vector3.left * MoY);
        }
    }
}
