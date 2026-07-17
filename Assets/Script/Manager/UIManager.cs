using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Button DEPButton; // 출항
    public Transform spaceshipTransform; // 우주선 위치값
    public Text TimeText; // 도착 남은 시간

    public Button EnterButton; // 우주선 탑승
    public Button ExitButton; // 우주선 내리기

    public GameObject MainCamera; // 메인 카메라
    public GameObject SubCamera; // 서브 카메라

    public GameObject PlayerOBJ; // 플레이어 오브젝트
    public GameObject InventoryUI; // 인벤토리 UI
    public GameObject UpgradeUI; // 업그레이드 UI

    public Text O2Text; // 산소 텍스트
    public Image O2Image; // 산소 이미지

    public Image boosterBar; // 부스터바

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
        O2Image.fillAmount = GameManager.Instance.O2 / GameManager.Instance.UpgradeManager.MaxO2[GameManager.Instance.UpgradeManager.O2Level];
        boosterBar.fillAmount = GameManager.Instance.BoosterTimer /  7;

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
            GameManager.Instance.currentTarget = GameManager.Instance.moonTransform; // 현재타겟을 달위치로
            GameManager.Instance.isSpacebooster = true; // 우주선 부스터 작동
            DEPButton.gameObject.SetActive(false);
            TimeText.gameObject.SetActive(true);
        }

        if (index == 2) // 화성 이동
        {
            GameManager.Instance.currentTarget = GameManager.Instance.marsTransform; // 현재타겟을 화성위치로
            GameManager.Instance.isSpacebooster = true; // 우주선 부스터 작동
        }
    }

    public void ExitButtons(int x)
    {

        if (x == 0) // 우주선 타기
        {
            PlayerOBJ.SetActive(false); // 플레이어 오브젝트 비활성

            DEPButton.gameObject.SetActive(true); // 출항버튼 활성
            boosterBar.gameObject.SetActive(true); // 부스터바 활성

            EnterButton.gameObject.SetActive(false); // 탑승버튼 비활성
            ExitButton.gameObject.SetActive(true); // 나가기버튼 활성

            MainCamera.SetActive(false); // 메인카메라 비활성
            SubCamera.SetActive(true); // 서브카메라 활성

            GameManager.Instance.O2 = GameManager.Instance.UpgradeManager.MaxO2[GameManager.Instance.UpgradeManager.O2Level];
            GameManager.Instance.isO2 = false;

        }

        if (x == 1) // 우주선 내리기 버튼
        {
            PlayerOBJ.transform.position = spaceshipTransform.TransformPoint(new Vector3(-3f, 0f, 0f));
            PlayerOBJ.SetActive(true); // 플레이어 오브젝트 활성

            DEPButton.gameObject.SetActive(false); // 출항 비활성
            TimeText.gameObject.SetActive(false); // 남은 도착 시간 숨김
            boosterBar.gameObject.SetActive(false); // 부스터바 숨김

            ExitButton.gameObject.SetActive(false); // 나가기버튼 비활성

            MainCamera.SetActive(true); // 메인카메라 활성
            SubCamera.SetActive(false); // 서브카메라 비활성

            GameManager.Instance.isO2 = true;

        }
    }
}
