using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
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

    public GameObject engineOBJ;
    public GameObject wallOBJ;
    public GameObject O2OBJ;
    public GameObject robotOBJ;

    // { 엔진과 외벽이 같은 레벨 이상일 경우 우주선 외형 변경 }

    void Start()
    {
        GameManager.Instance.spaceshipSpeed = engine[engineLevel]; // 우주선 속도
    }

    public void EngineUpgrade()
    {
        if (engineLevel < 3)
        {
            if (engineLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.copper[0] >= 3)
            {
                // 철 Lv1 3개 + 구리 Lv1 3개
                engineLevel++;
                GameManager.Instance.spaceshipSpeed = engine[engineLevel]; // 우주선 속도
                GameManager.Instance.Inventory.iron[0] -= 3; // 철 3개 차감
                GameManager.Instance.Inventory.copper[0] -= 3; // 구리 3개 차감
                
            }
            else if (engineLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.copper[1] >= 3)
            {
                // 철 Lv2 3개 + 구리 Lv2 3개
                engineLevel++;
                GameManager.Instance.spaceshipSpeed = engine[engineLevel]; // 우주선 속도
                GameManager.Instance.Inventory.iron[1] -= 3; // 철 3개 차감
                GameManager.Instance.Inventory.copper[1] -= 3; // 구리 3개 차감                           
            }
            else Debug.Log("자원 부족함. {나중에 팝업메시지 추가할 예정}");
        }
    }

    public void WallUpgrade()
    {
        if (wallLevel < 3)
        {
            if (wallLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.plastic[0] >= 3)
            {
                // 철 Lv1 3개 + 플라스틱 Lv1 3개
                wallLevel++;
                GameManager.Instance.Inventory.iron[0] -= 3;
                GameManager.Instance.Inventory.plastic[0] -= 3;
            }
            else if (wallLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.plastic[1] >= 3)
            {
                // 철 Lv2 3개 + 플라스틱 Lv2 3개
                wallLevel++;
                GameManager.Instance.Inventory.iron[1] -= 3;
                GameManager.Instance.Inventory.plastic[1] -= 3;

            }
            else Debug.Log("자원부족");
        }
    }

    public void O2Upgrade()
    {
        if (O2Level < 3)
        {
            if (O2Level == 1 && GameManager.Instance.Inventory.copper[0] >= 3 && GameManager.Instance.Inventory.plastic[0] >= 3)
            {
                // 구리 Lv1 3개 + 플라스틱 Lv1 3개
                O2Level++;
                GameManager.Instance.O2 = MaxO2[O2Level];
                GameManager.Instance.Inventory.copper[0] -= 3;
                GameManager.Instance.Inventory.plastic[0] -= 3;
            }
            else if (O2Level == 2 && GameManager.Instance.Inventory.copper[1] >= 3 && GameManager.Instance.Inventory.plastic[1] >= 3)
            {
                // 구리 Lv2 3개 + 플라스틱 Lv2 3개
                O2Level++;
                GameManager.Instance.O2 = MaxO2[O2Level];
                GameManager.Instance.Inventory.copper[1] -= 3;
                GameManager.Instance.Inventory.plastic[1] -= 3;
            }
            else Debug.Log("자원부족");

        }


    }

    public void SuitUpgrade()
    {       
        if (suitLevel < 3)
        {
            if (suitLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.plastic[0] >= 3)
            {
                // 철 Lv1 3개 + 플라스틱 Lv1 3개
                suitLevel++;
                GameManager.Instance.Kg = Suit[suitLevel];
                GameManager.Instance.Inventory.iron[0] -= 3;
                GameManager.Instance.Inventory.plastic[0] -= 3;
            }
            else if (suitLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.plastic[1] >= 3)
            {
                // 철 Lv2 3개 + 플라스틱 Lv2 3개
                suitLevel++;
                GameManager.Instance.Kg = Suit[suitLevel];
                GameManager.Instance.Inventory.iron[1] -= 3;
                GameManager.Instance.Inventory.plastic[1] -= 3;
            }
        }
    }

    public void RobotUpgrade()
    {
        if (robotLevel < 3)
        {
            if (robotLevel == 0 && GameManager.Instance.Inventory.iron[0] >= 2 && GameManager.Instance.Inventory.copper[0] >= 2)
            {
                // 철 Lv1 2개 + 구리 Lv1 2개
                robotLevel++;
                GameManager.Instance.Inventory.iron[0] -= 2;
                GameManager.Instance.Inventory.copper[0] -= 2;
            }
            else if (robotLevel == 1 && GameManager.Instance.Inventory.iron[0] >= 3 && GameManager.Instance.Inventory.copper[0] >= 3)
            {
                // 철 Lv1 3개 + 구리 Lv1 3개
                robotLevel++;
                GameManager.Instance.Inventory.iron[0] -= 3;
                GameManager.Instance.Inventory.copper[0] -= 3;
            }
            else if (robotLevel == 2 && GameManager.Instance.Inventory.iron[1] >= 3 && GameManager.Instance.Inventory.copper[1] >= 3)
            {
                // 철 Lv2 3개 + 구리 Lv2 3개
                robotLevel++;
                GameManager.Instance.Inventory.iron[1] -= 3;
                GameManager.Instance.Inventory.copper[1] -= 3;
            }

        }
    }

    public int GetSuit()
    {
        return Suit[suitLevel];
    }
}
