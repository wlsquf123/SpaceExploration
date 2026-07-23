using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public MiniGame miniGame;

    [Header("레벨")]
    public int engineLevel = 1;
    public int wallLevel = 1;
    public int O2Level = 1;
    public int suitLevel = 1;
    public int robotLevel = 0;

    [Header("레벨별 능력치")]
    public int[] engine = {0, 100, 120, 150 }; // 엔진
    public int[] wall = {0, 50, 70, 100 }; // 외벽
    public float[] MaxO2 = {0, 100, 200, 300 }; // 산소총량
    public int[] Suit = { 0, 100, 200, 300 }; // 우주복
    public int[] robot = { 0, 1, 2, 3 }; // 채집로봇

    public float Timer;

    public bool engineBreakdown = false; // 고장 여부
    public bool wallBreakdown = false;
    public bool O2Breakdown = false;
    public bool robotBreakdown = false;
    public int robotCount; // 자동채집 카운트 (숫자세기)

    public GameObject MessageBox;

    [Header("미니게임")]
    public GameObject letter; // 글자 맞추기
    public GameObject card; // 카드 게임
    public GameObject MoveBar; // 움직이는 바 맞추기

    [Header("글자 맞추기 게임")]
    public Text RandomletterText;
    public InputField letterInput;
    public Button letterButton;

    private string[] words = { "바나나", "사과", "옥수수", "딸기", "수박" };
    private string answer;

    // { 엔진과 외벽이 같은 레벨 이상일 경우 우주선 외형 변경 }

    void Start()
    {
        GameManager.Instance.spaceshipSpeed = engine[engineLevel]; // 우주선 속도
    }

    private void Update()
    {
        Breaks();
    }

    public void MessagePrefab(string Msg, string SubMessage) // 고장 메시지 출력
    {
        GameObject MsgBox = Instantiate(MessageBox);

        MsgBox.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = Msg;
        MsgBox.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = SubMessage;

        Destroy(MsgBox, 3f);
    }

    public void Breaks() // 고장
    {
        // 채집로봇 고장
        if (robotBreakdown == false)
        {
            if (robotCount >= 50)
            {
                robotBreakdown = true;
                MessagePrefab("채집로봇 시스템 고장!", "자동채집 불가");
            }
        }
        if (GameManager.Instance.isFlying == false) return;
        Timer += Time.deltaTime;

        if (Timer >= 1)
        {
            // 엔진 고장
            if (engineBreakdown == false)
            {
                int r = Random.Range(0, 100);
                if (r < 3)
                {
                    engineBreakdown = true;
                    MessagePrefab("엔진 시스템 고장!", "우주선의 기본 속도 50% 감소\n엔진 부스터 사용 불가");
                }
            }

            // 외벽 고장
            if (wallBreakdown == false)
            {
                int r = Random.Range(0, 100);
                if (r < 2)
                {
                    wallBreakdown = true;
                    MessagePrefab("외벽 시스템 고장!", "엔진 부스터 사용 불가");
                }
            }

            // 산소 고장
            if (O2Breakdown == false)
            {
                int r = Random.Range(0, 100);
                if (r < 1)
                {
                    O2Breakdown = true;
                    MessagePrefab("산소탱크 시스템 고장!", "산소 회복 초당 10으로 변경\n엔진 부스터 사용 불가");
                }
            }
            Timer = 0;
        }
    }

    public void UpgradeCountButton(int index)
    {
        if (engineLevel < 3 && index == 1)
        {
            if (engineLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.copper[0] >= 3)
            {
                // 철 Lv1 3개 + 구리 Lv1 3개
                engineLevel++;
                GameManager.Instance.spaceshipSpeed = engine[engineLevel]; // 우주선 속도
                GameManager.Instance.Inventory.iron[0] -= 3; // 철 3개 차감
                GameManager.Instance.Inventory.copper[0] -= 3; // 구리 3개 차감
                GameManager.Instance.UIManager.EnginePanel.SetActive(false);
                GameManager.Instance.UIManager.EnginePanel2.SetActive(true);
            }
            else if (engineLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.copper[1] >= 3)
            {
                // 철 Lv2 3개 + 구리 Lv2 3개
                engineLevel++;
                GameManager.Instance.spaceshipSpeed = engine[engineLevel]; // 우주선 속도
                GameManager.Instance.Inventory.iron[1] -= 3; // 철 3개 차감
                GameManager.Instance.Inventory.copper[1] -= 3; // 구리 3개 차감 
                GameManager.Instance.UIManager.EnginePanel2.SetActive(false);
                GameManager.Instance.UIManager.EndPanel.SetActive(true);
            }
            else Debug.Log("자원 부족함. {나중에 팝업메시지 추가할 예정}");
        }

        if (wallLevel < 3 && index == 2)
        {
            if (wallLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.plastic[0] >= 3)
            {
                // 철 Lv1 3개 + 플라스틱 Lv1 3개
                wallLevel++;
                GameManager.Instance.Inventory.iron[0] -= 3;
                GameManager.Instance.Inventory.plastic[0] -= 3;
                GameManager.Instance.UIManager.wallPanel.SetActive(false);
                GameManager.Instance.UIManager.wallPanel2.SetActive(true);
            }
            else if (wallLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.plastic[1] >= 3)
            {
                // 철 Lv2 3개 + 플라스틱 Lv2 3개
                wallLevel++;
                GameManager.Instance.Inventory.iron[1] -= 3;
                GameManager.Instance.Inventory.plastic[1] -= 3;
                GameManager.Instance.UIManager.wallPanel2.SetActive(false);
                GameManager.Instance.UIManager.EndPanel.SetActive(true);
            }
            else Debug.Log("자원부족");
        }

        if (O2Level < 3 && index == 3)
        {
            if (O2Level == 1 && GameManager.Instance.Inventory.copper[0] >= 3 && GameManager.Instance.Inventory.plastic[0] >= 3)
            {
                // 구리 Lv1 3개 + 플라스틱 Lv1 3개
                O2Level++;
                GameManager.Instance.O2 = MaxO2[O2Level];
                GameManager.Instance.Inventory.copper[0] -= 3;
                GameManager.Instance.Inventory.plastic[0] -= 3;
                GameManager.Instance.UIManager.O2Panel.SetActive(false);
                GameManager.Instance.UIManager.O2Panel2.SetActive(true);
            }
            else if (O2Level == 2 && GameManager.Instance.Inventory.copper[1] >= 3 && GameManager.Instance.Inventory.plastic[1] >= 3)
            {
                // 구리 Lv2 3개 + 플라스틱 Lv2 3개
                O2Level++;
                GameManager.Instance.O2 = MaxO2[O2Level];
                GameManager.Instance.Inventory.copper[1] -= 3;
                GameManager.Instance.Inventory.plastic[1] -= 3;
                GameManager.Instance.UIManager.O2Panel2.SetActive(false);
                GameManager.Instance.UIManager.EndPanel.SetActive(true);
            }
            else Debug.Log("자원부족");
        }

        if (suitLevel < 3 && index == 4)
        {
            if (suitLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.plastic[0] >= 3)
            {
                // 철 Lv1 3개 + 플라스틱 Lv1 3개
                suitLevel++;
                GameManager.Instance.Kg = Suit[suitLevel];
                GameManager.Instance.Inventory.iron[0] -= 3;
                GameManager.Instance.Inventory.plastic[0] -= 3;
                GameManager.Instance.UIManager.suitPanel.SetActive(false);
                GameManager.Instance.UIManager.suitPanel2.SetActive(true);
            }
            else if (suitLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.plastic[1] >= 3)
            {
                // 철 Lv2 3개 + 플라스틱 Lv2 3개
                suitLevel++;
                GameManager.Instance.Kg = Suit[suitLevel];
                GameManager.Instance.Inventory.iron[1] -= 3;
                GameManager.Instance.Inventory.plastic[1] -= 3;
                GameManager.Instance.UIManager.suitPanel2.SetActive(false);
                GameManager.Instance.UIManager.EndPanel.SetActive(true);
            }
        }

        if (robotLevel < 3 && index == 5)
        {
            if (robotLevel == 0 && GameManager.Instance.Inventory.iron[0] >= 2 && GameManager.Instance.Inventory.copper[0] >= 2)
            {
                // 철 Lv1 2개 + 구리 Lv1 2개
                robotLevel++;
                GameManager.Instance.Inventory.iron[0] -= 2;
                GameManager.Instance.Inventory.copper[0] -= 2;
                GameManager.Instance.UIManager.robotPanel.SetActive(false);
                GameManager.Instance.UIManager.robotPanel2.SetActive(true);
            }
            else if (robotLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.copper[0] >= 3)
            {
                // 철 Lv1 3개 + 구리 Lv1 3개
                robotLevel++;
                GameManager.Instance.Inventory.iron[0] -= 3;
                GameManager.Instance.Inventory.copper[0] -= 3;
                GameManager.Instance.UIManager.robotPanel2.SetActive(false);
                GameManager.Instance.UIManager.robotPanel3.SetActive(true);
            }
            else if (robotLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.copper[1] >= 3)
            {
                // 철 Lv2 3개 + 구리 Lv2 3개
                robotLevel++;
                GameManager.Instance.Inventory.iron[1] -= 3;
                GameManager.Instance.Inventory.copper[1] -= 3;
                GameManager.Instance.UIManager.robotPanel3.SetActive(false);
                GameManager.Instance.UIManager.EndPanel.SetActive(true);
            }
        }
    }

    public void RepairButton(int index)
    {

        if (GameManager.Instance.Inventory.iron[0] < 2 || GameManager.Instance.Inventory.copper[0] < 2 || GameManager.Instance.Inventory.plastic[0] < 2)
        {
            Debug.Log("수리 자원 부족!");
            return;
        }

        GameManager.Instance.Inventory.iron[0] -= 2;
        GameManager.Instance.Inventory.copper[0] -= 2;
        GameManager.Instance.Inventory.plastic[0] -= 2; 

            RandomMiniGames(index);
        switch (index)
        {
            case 1:
                
                engineBreakdown = false;
                GameManager.Instance.UIManager.engineImageRepair.gameObject.SetActive(false);
                break;
            case 2:
                wallBreakdown = false;
                GameManager.Instance.UIManager.wallImageRepair.gameObject.SetActive(false);
                break;
            case 3:
                O2Breakdown = false;
                GameManager.Instance.UIManager.O2ImageRepair.gameObject.SetActive(false);
                break;
            case 4:
                robotBreakdown = false;
                GameManager.Instance.UIManager.robotImageRepair.gameObject.SetActive(false);
                break;
        }
    }

    public void RandomMiniGames(int x)
    {
        int random = Random.Range(1, 2);

        switch (random)
        {
            case 1: // 글자 맞추기
                letter.SetActive(true);
                answer = words[Random.Range(0, words.Length)];
                RandomletterText.text = answer;
                letterInput.text = "";
                if (letterInput.text == answer)
                {
                    Debug.Log("수리 성공!");
                    letter.SetActive(false); // 글자 맞추기 창 나가기
                }
                else
                {
                    Debug.Log("수리 실패!");
                    letter.SetActive(false); // 글자 맞추기 창 나가기
                }
                break;
        }
    }

    public void CheckLetter()
    {
        
    }

    public int GetSuit()
    {
        return Suit[suitLevel];
    }
}
