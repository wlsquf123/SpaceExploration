using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moon"))
        {
            SceneManager.LoadScene("Moon");
            GameManager.Instance.UIManager.ExitButton.gameObject.SetActive(true);
        }
    }


}
