using UnityEngine;
using UnityEngine.UI;

public class DisplayEnterButton : MonoBehaviour
{
    public GameObject EnterButton;

    private void Awake()
    {
        var uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        EnterButton = uiManager.EnterButton.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        EnterButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        EnterButton.SetActive(false);
    }
}
