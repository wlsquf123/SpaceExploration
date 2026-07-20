using UnityEngine;
using UnityEngine.UI;

public class DisplayEnterButton : MonoBehaviour
{
    float Timer;
    private void Update()
    {
        Timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; // 만약에 플레이어 태그가 아니면 돌아가라 아래코드 실행 ㄴㄴ
        GameManager.Instance.isO2 = false;
        GameManager.Instance.UIManager.EnterButton.gameObject.SetActive(true);

        if (GameManager.Instance.UpgradeManager.O2Breakdown == false)
        {
            GameManager.Instance.O2 = GameManager.Instance.UpgradeManager.MaxO2[GameManager.Instance.UpgradeManager.O2Level];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance.UpgradeManager.O2Breakdown == true)
        {
            if (GameManager.Instance.O2 < GameManager.Instance.UpgradeManager.MaxO2[GameManager.Instance.UpgradeManager.O2Level])
            {
                GameManager.Instance.O2 += 10 * Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.isO2 = true;
        GameManager.Instance.UIManager.EnterButton.gameObject.SetActive(false);
    }
}
