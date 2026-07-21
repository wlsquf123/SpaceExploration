using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float rotationSpeed = 1f; // 배경 회전 속도

    private float rotation;

    void Start()
    {
        rotation = 0;
        rotation = RenderSettings.skybox.GetFloat("_Rotation");
    }

    void Update()
    {
        rotation += rotationSpeed * Time.deltaTime;

        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}