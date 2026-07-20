using UnityEngine;

public class SpaceshipAutomation : MonoBehaviour
{
    public float turnSpeed = 1.0f;     // 회전 속도

    void Update()
    {
        Transform target = GameManager.Instance.currentTarget; // 현재 타겟

        // 목표가 없으면 실행하지 않음
        if (target == null) return;

        // 목표 방향
        Vector3 direction = target.position - transform.position;

        // 목표 방향으로 회전
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        if (GameManager.Instance.UpgradeManager.engineBreakdown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, GameManager.Instance.spaceshipSpeed * Time.deltaTime);
        }
        else
        {
            // 목표 위치를 향해 이동
            transform.position = Vector3.MoveTowards(transform.position, target.position, GameManager.Instance.spaceshipSpeed/2 * Time.deltaTime);
        }
            
        // 목표 위치에 정확히 도착
        if (transform.position == target.position)
        {
            GameManager.Instance.Arrived();
        }
    }
}