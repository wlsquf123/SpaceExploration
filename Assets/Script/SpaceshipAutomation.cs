using UnityEngine;

public class SpaceshipAutomation : MonoBehaviour
{
    float moveSpeed;    // 전진 속도
    public float turnSpeed = 1.0f;     // 회전 속도

    void Update()
    {
        moveSpeed = GameManager.Instance.spaceshipSpeed;

        if (GameManager.Instance.isFlying == false) return;

        Transform target = GameManager.Instance.currentTarget;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= GameManager.Instance.di)
        {
            GameManager.Instance.Arrived();
            return;
        }

        Vector3 directionToMoon = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToMoon);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}