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

    public Button EnterButton; // 우주선 탑승
    public Button ExitButton; // 우주선 내리기

    public Text statusText; // 도착 남은 시간
    public Text O2Text; // 산소 텍스트
    public Image O2Image; // 산소 이미지

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        State(); // 상태

        if (GameManager.Instance.isFlying && GameManager.Instance.currentTarget != null)
        {
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
        if (index == 1) // 달 이동
        {
            GameManager.Instance.currentTarget = GameManager.Instance.moonObject;
            GameManager.Instance.isFlying = true;
            DEPButton.gameObject.SetActive(false);
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
            statusText.gameObject.SetActive(false); // 남은 도착 시간 숨김
            DEPButton.gameObject.SetActive(false); // 출항 숨김
            ExitButton.gameObject.SetActive(false);
        }
    }
}
