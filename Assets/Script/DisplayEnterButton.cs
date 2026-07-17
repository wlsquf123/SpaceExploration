using UnityEngine;
using UnityEngine.UI;

public class DisplayEnterButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; // 만약에 플레이어 태그가 아니면 돌아가라 아래코드 실행 ㄴㄴ
        GameManager.Instance.UIManager.EnterButton.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.UIManager.EnterButton.gameObject.SetActive(false);
    }
}
