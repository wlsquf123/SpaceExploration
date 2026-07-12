using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager UIManager;
    public UpgradeManager UpgradeManager;
    public SceneManage SceneManager;

    public Transform moonObject; // 달 오브젝트

    public Transform currentTarget;     // 현재 목적지
    public bool isFlying = false;       // 지금 날아가는 중인가?

    public float spaceshipSpeed; // 속도
    public float di = 500f; // 범위

    public float MaxO2 = 100f;
    public float O2 = 0; // 산소


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        O2 = MaxO2;
    }

    private void Update()
    {
        O2 -= Time.deltaTime; // 산소 감소

        if (O2 <= 0)
        {
            Time.timeScale = 0f;
            UIManager.ExitButtons(0);
        }
    }

    public void Arrived()
    {
        isFlying = false;
        currentTarget = null;
        UIManager.DEPButton.gameObject.SetActive(true);
    }
}
