using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject UpgradeButton; // 업그레이드 버튼

    public Text O2Text; // 산소 텍스트
    public Image O2Image; // 산소 이미지

    public Image boosterBar; // 부스터바

    public Text KgText; // 무게 텍스트

    [Header("자원 텍스트")]
    public Text[] ironText = new Text[3]; // 철 텍스트
    public Text[] copperText = new Text[3]; // 구리 텍스트
    public Text[] plasticText = new Text[3]; // 플라스틱 텍스트
    public Text[] coreText = new Text[2]; // 코어 텍스트

    [Header("업그레이드 시스템 UI 판넬")]
    public GameObject EnginePanel;
    public GameObject EnginePanel2;
    public GameObject wallPanel;
    public GameObject wallPanel2;
    public GameObject O2Panel;
    public GameObject O2Panel2;
    public GameObject suitPanel;
    public GameObject suitPanel2;
    public GameObject robotPanel;
    public GameObject robotPanel2;
    public GameObject robotPanel3;
    public GameObject EndPanel;

    [Header("고장 텍스트")]
    public Text engineWrong;
    public Text wallWrong;
    public Text O2Wrong;
    public Text robotWrong;

    [Header("수리 버튼")]
    public Button engineButtonRepair;
    public Button wallButtonRepair;
    public Button O2ButtonRepair;
    public Button robotButtonRepair;

    [Header("수리 이미지")]
    public Image engineImageRepair;
    public Image wallImageRepair;
    public Image O2ImageRepair;
    public Image robotImageRepair;


    // Update is called once per frame
    void Update()
    {
        State(); // 상태

        if (GameManager.Instance.currentTarget != null)
        {
            // 우주선과 목표 지점 사이의 실제 거리
            float distance = Vector3.Distance(spaceshipTransform.position, GameManager.Instance.currentTarget.position);
            float remainingTime;
            if (GameManager.Instance.UpgradeManager.engineBreakdown == false)
            {
                remainingTime = distance / GameManager.Instance.spaceshipSpeed; // 시간 = 거리 / 속도
            }
            else
            {
                remainingTime = distance / (GameManager.Instance.spaceshipSpeed / 2);
            }
            TimeText.text = "도착까지 남은시간: " + remainingTime.ToString("F1") + "초";
        }
        else
        {
            TimeText.gameObject.SetActive(false);
        }
    }

    public void State()
    {
        O2Text.text = GameManager.Instance.O2.ToString("F0") + "%"; // 산소량
        O2Image.fillAmount = GameManager.Instance.O2 / GameManager.Instance.UpgradeManager.MaxO2[GameManager.Instance.UpgradeManager.O2Level];
        boosterBar.fillAmount = GameManager.Instance.BoosterTimer / 7f;

        int kgLevel;
        if (GameManager.Instance.Inventory.InventoryKg() / GameManager.Instance.UpgradeManager.GetSuit() * 100 >= 81)
        {
            kgLevel = 3;
        }
        else if (GameManager.Instance.Inventory.InventoryKg() / GameManager.Instance.UpgradeManager.GetSuit() * 100 >= 41)
        {
            kgLevel = 2;
        }
        else
        {
            kgLevel = 1;
        }

        KgText.text = "인벤토리\n" + kgLevel + "단계 " + GameManager.Instance.Inventory.InventoryKg().ToString() + "kg";

        for (int i = 0; i < 3; i++) // 자원 텍스트
        {
            ironText[i].text = GameManager.Instance.Inventory.iron[i].ToString();
            copperText[i].text = GameManager.Instance.Inventory.copper[i].ToString();
            plasticText[i].text = GameManager.Instance.Inventory.plastic[i].ToString();
        }
        coreText[0].text = GameManager.Instance.Inventory.core[0].ToString();
        coreText[1].text = GameManager.Instance.Inventory.core[1].ToString();

        UpgradeBreakState(engineWrong, GameManager.Instance.UpgradeManager.engineBreakdown, engineButtonRepair); // 엔진 고장상태 여부 시스템 
        UpgradeBreakState(wallWrong, GameManager.Instance.UpgradeManager.wallBreakdown, wallButtonRepair); // 외벽 고장상태 여부
        UpgradeBreakState(O2Wrong, GameManager.Instance.UpgradeManager.O2Breakdown, O2ButtonRepair); // 산소 고장 여부
        UpgradeBreakState(robotWrong, GameManager.Instance.UpgradeManager.robotBreakdown, robotButtonRepair); // 채집로봇 고장 여부
    }

    public void UpgradeBreakState(Text stateText, bool isBreak, Button repair)
    {
        if (isBreak == false)
        {
            stateText.text = "정상";
            stateText.color = Color.green;
            repair.gameObject.SetActive(false);
        }
        else
        {
            stateText.text = "고장";
            stateText.color = Color.red;
            repair.gameObject.SetActive(true);
        }
    }

    public void SelectDestination(int index)
    {
        GameManager.Instance.isFlying = true; // 나는중
        GameManager.Instance.isSpacebooster = true; // 우주선 부스터 작동
        DEPButton.gameObject.SetActive(false);
        TimeText.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(false); // 나가기버튼 숨김
        UpgradeButton.SetActive(false); // 업그레이드버튼 숨김

        switch (index) // 우주선 위치 이동
        {
            case 1:
                GameManager.Instance.currentTarget = GameManager.Instance.moonTransform;
                break;
            case 2:
                GameManager.Instance.currentTarget = GameManager.Instance.marsTransform; // 현재타겟을 화성위치로
                break;
            case 3:
                // 추가 예정
                break;
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

            GameManager.Instance.isO2 = false;
            UpgradeButton.SetActive(true);

        }

        if (x == 1) // 우주선 내리기 버튼
        {
            PlayerOBJ.transform.position = spaceshipTransform.TransformPoint(new Vector3(-1.3f, 1f, 0f));
            PlayerOBJ.SetActive(true); // 플레이어 오브젝트 활성

            DEPButton.gameObject.SetActive(false); // 출항 비활성
            TimeText.gameObject.SetActive(false); // 남은 도착 시간 숨김
            boosterBar.gameObject.SetActive(false); // 부스터바 숨김

            ExitButton.gameObject.SetActive(false); // 나가기버튼 비활성

            MainCamera.SetActive(true); // 메인카메라 활성
            SubCamera.SetActive(false); // 서브카메라 비활성

            GameManager.Instance.isO2 = true;
            UpgradeButton.SetActive(false);
        }
    }

    public void UpgradeButtons(int index)
    {
        EnginePanel.SetActive(false);
        EnginePanel2.SetActive(false);

        wallPanel.SetActive(false);
        wallPanel2.SetActive(false);

        O2Panel.SetActive(false);
        O2Panel2.SetActive(false);

        suitPanel.SetActive(false);
        suitPanel2.SetActive(false);

        robotPanel.SetActive(false);
        robotPanel2.SetActive(false);
        robotPanel3.SetActive(false);

        EndPanel.SetActive(false);

        switch (index)
        {
            case 1:
                if (GameManager.Instance.UpgradeManager.engineLevel == 1)
                {
                    EnginePanel.SetActive(true);
                }
                else if (GameManager.Instance.UpgradeManager.engineLevel == 2)
                {
                    EnginePanel2.SetActive(true);
                }
                else
                {
                    EndPanel.SetActive(true);
                }
                break;
            case 2:
                if (GameManager.Instance.UpgradeManager.wallLevel == 1)
                {
                    wallPanel.SetActive(true);
                }
                else if (GameManager.Instance.UpgradeManager.wallLevel == 2)
                {
                    wallPanel2.SetActive(true);
                }
                else
                {
                    EndPanel.SetActive(true);
                }
                break;
            case 3:
                if (GameManager.Instance.UpgradeManager.O2Level == 1)
                {
                    O2Panel.SetActive(true);
                }
                else if (GameManager.Instance.UpgradeManager.O2Level == 2)
                {
                    O2Panel2.SetActive(true);
                }
                else
                {
                    EndPanel.SetActive(true);
                }
                break;
            case 4:
                if (GameManager.Instance.UpgradeManager.suitLevel == 1)
                {
                    suitPanel.SetActive(true);
                }
                else if (GameManager.Instance.UpgradeManager.suitLevel == 2)
                {
                    suitPanel2.SetActive(true);
                }
                else
                {
                    EndPanel.SetActive(true);
                }
                break;
            case 5:
                if (GameManager.Instance.UpgradeManager.robotLevel == 0)
                {
                    robotPanel.SetActive(true);
                }
                else if (GameManager.Instance.UpgradeManager.robotLevel == 1)
                {
                    robotPanel2.SetActive(true);
                }
                else if (GameManager.Instance.UpgradeManager.robotLevel == 2)
                {
                    robotPanel3.SetActive(true);
                }
                else
                {
                    EndPanel.SetActive(true);
                }
                break;
        }
    }
}
