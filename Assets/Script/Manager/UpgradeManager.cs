using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public float MaxCo2 = 100f;
    public float O2 = 0; // 산소
    public int Suit; // 우주복
    public int robot = 0; // 채집로봇 (시작 0레벨)

    /* 
     * 초기 레벨 1
     * 자원으로 레벨 업그레이드
     * 자원 ( 돌, 플라스틱, 금속 )  ** 게임 시작 시 플레이어에게 기초 자원 각각 2개씩 지급
     * 
     * 
    */



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
