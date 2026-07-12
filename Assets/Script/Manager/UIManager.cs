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

    public Button EnterButton;
    public Button ExitButton; 

    public Text statusText; // 도착 남은 시간
    public Text O2Text; // 산소 텍스트
    public Image O2Image; // 산소 이미지

    

    // Update is called once per frame
    void Update()
    {
        State(); // 상태

        if (GameManager.Instance.isFlying && GameManager.Instance.currentTarget != null)
        {
            // [체크] 혹시 우주선 연결이 빠졌을 때를 대비한 안전장치
            if (spaceshipTransform == null) return;

            // ⭐ [수정] transform.position 대신 spaceshipTransform.position을 사용합니다!
            float distance = Vector3.Distance(spaceshipTransform.position, GameManager.Instance.currentTarget.position);

            // 정지 거리가 300이므로 남은 거리 보정
            float remainingDistance = Mathf.Max(0, distance - GameManager.Instance.di);

            // 시간 = 거리 / 속도
            float remainingTime = remainingDistance / GameManager.Instance.spaceshipSpeed;

            statusText.text = $"도착까지 남은 시간: {remainingTime.ToString("F1")}초";
        }
        else
        {
            statusText.text = "";
        }
    }

    public void State()
    {
        O2Text.text = GameManager.Instance.O2.ToString("F0") + "%"; // 산소량
        O2Image.fillAmount = GameManager.Instance.O2 / GameManager.Instance.MaxO2;

    }

    public void SelectDestination(int index)
    {
        if (index == 1)
        {
            GameManager.Instance.currentTarget = GameManager.Instance.moonObject;
            GameManager.Instance.isFlying = true;
            DEPButton.gameObject.SetActive(false);
        }
    }

    public void ExitButtons(int x)
    {

        if (x == 1) // 내리기
        {
            // 산소량 추가 코드 = true
            MainCamera.SetActive(true);
            SubCamera.SetActive(false);
            PlayerOBJ.SetActive(true);
            statusText.gameObject.SetActive(false); // 남은 도착 시간 숨김
            DEPButton.gameObject.SetActive(false); // 출항 숨김
            ExitButton.gameObject.SetActive(false); // 내리기 버튼 숨김


        }

        if (x == 0) // 타기
        {
            MainCamera.SetActive(false); // 메인카메라 숨김
            SubCamera.SetActive(true); // 서브카메라 활성화
            PlayerOBJ.SetActive(false);
            DEPButton.gameObject.SetActive(true);
            EnterButton.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(true);

        }
    }
}
