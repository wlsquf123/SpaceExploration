using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public int[] engine = { 100, 120, 150 }; // 엔진
    public int[] wall = { 50, 70, 100 }; // 외벽
    public float[] MaxO2 = { 100, 200, 300 }; // 산소총량
    public float O2 = 0; // 산소
    public int[] Suit = {  }; // 우주복 *확률 계산 모르겠어서 넘김
    public int robot = 0; // 채집로봇 (0레벨) * 자원 랜덤수집이라 넘김 

    // 엔진과 외벽이 같은 레벨 이상일 경우 우주선 외형 변경





    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Co2Upgrade(float x)
    {
        // if문 나중 추가 (자원)
        GameManager.Instance.MaxO2 = x;
        GameManager.Instance.O2 = GameManager.Instance.MaxO2;
    }
}
