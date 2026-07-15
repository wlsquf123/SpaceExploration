using UnityEngine;
using UnityEngine.UI;

public class DisplayEnterButton : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.UIManager.EnterButton.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.UIManager.EnterButton.gameObject.SetActive(false);
    }
}
