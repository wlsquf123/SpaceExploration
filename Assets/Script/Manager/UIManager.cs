using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button DEPButton; // 출항

    public Transform spaceshipTransform;

    public GameObject MainCamera; // 메인 카메라
    public GameObject SubCamera; // 서브 카메라

    public GameObject PlayerOBJ; // 플레이어 오브젝트
    public GameObject InventoryOBJ; // 인벤토리 오브젝트

    public Button EnterButton; // 우주선 탑승
    public Button ExitButton; // 우주선 내리기

    public Text TimeText; // 도착 남은 시간
    public Text O2Text; // 산소 텍스트
    public Image O2Image; // 산소 이미지

    public Text[] ironText = new Text[3]; // 철 텍스트
    public Text[] copperText = new Text[3]; // 구리 텍스트
    public Text[] plasticText = new Text[3]; // 플라스틱 텍스트
    public Text[] coreText = new Text[2]; // 코어 텍스트

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        State(); // 상태

        if (GameManager.Instance.currentTarget != null)
        {
            // 우주선과 목표 지점 사이의 실제 거리
            float distance = Vector3.Distance(spaceshipTransform.position, GameManager.Instance.currentTarget.position);

            // 시간 = 거리 / 속도
            float remainingTime = distance / GameManager.Instance.spaceshipSpeed;

            TimeText.text = "도착까지 남은시간: " + remainingTime.ToString("F1") + "초";
        }
        else
        {
            TimeText.text = "";
        }
    }

    public void State()
    {
        O2Text.text = GameManager.Instance.O2.ToString("F0") + "%"; // 산소량
        O2Image.fillAmount = GameManager.Instance.O2 / GameManager.Instance.MaxO2;

        ironText[0].text = GameManager.Instance.Inventory.iron[0].ToString();
        ironText[1].text = GameManager.Instance.Inventory.iron[1].ToString();
        ironText[2].text = GameManager.Instance.Inventory.iron[2].ToString();

        copperText[0].text = GameManager.Instance.Inventory.copper[0].ToString();
        copperText[1].text = GameManager.Instance.Inventory.copper[1].ToString();
        copperText[2].text = GameManager.Instance.Inventory.copper[2].ToString();

        plasticText[0].text = GameManager.Instance.Inventory.plastic[0].ToString();
        plasticText[1].text = GameManager.Instance.Inventory.plastic[1].ToString();
        plasticText[2].text = GameManager.Instance.Inventory.plastic[2].ToString();

        coreText[0].text = GameManager.Instance.Inventory.core[0].ToString();
        coreText[1].text = GameManager.Instance.Inventory.core[1].ToString();
    }

    public void SelectDestination(int index)
    {
        if (index == 1) // 달 이동
        {
            GameManager.Instance.currentTarget = GameManager.Instance.moonObject;
            DEPButton.gameObject.SetActive(false);
            TimeText.gameObject.SetActive(true);
        }
    }

    public void ExitButtons(int x)
    {

        if (x == 0) // 우주선 타기
        {
            GameManager.Instance.O2 = GameManager.Instance.MaxO2;
            GameManager.Instance.isO2 = false;
            MainCamera.SetActive(false); // 메인카메라 숨김
            SubCamera.SetActive(true); // 서브카메라 활성화
            PlayerOBJ.SetActive(false);
            DEPButton.gameObject.SetActive(true);
            EnterButton.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(true);

        }

        if (x == 1) // 우주선 내리기
        {
            GameManager.Instance.isO2 = true;
            MainCamera.SetActive(true);
            SubCamera.SetActive(false);
            PlayerOBJ.SetActive(true);
            TimeText.gameObject.SetActive(false); // 남은 도착 시간 숨김
            DEPButton.gameObject.SetActive(false); // 출항 숨김
            ExitButton.gameObject.SetActive(false);
        }
    }
}
