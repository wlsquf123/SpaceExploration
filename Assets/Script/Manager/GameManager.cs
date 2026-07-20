using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager UIManager;
    public UpgradeManager UpgradeManager;
    public SceneManage SceneManager;
    public Inventory Inventory;

    public Transform moonTransform; // 달 위치
    public Transform marsTransform; // 화성 위치 
    public Transform europaTransform; // 유로파 위치 

    public Transform currentTarget;     // 현재 목적지
    public bool isFlying = false; // 날고 있는가?
    public float spaceshipSpeed; // 우주선 속도

    public GameObject boosterEffect; // 부스터파티클
    public bool isSpacebooster = false; // 탑승 부스터 사용 : 하선 부스터 사용안함
    public float BoosterTimer = 7f;

    public float O2 = 0; // 산소
    public bool isO2 = false; // 탑승 산소 100% 유지 : 하선 산소 작동

    public int Kg; // 무게

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        O2 = UpgradeManager.MaxO2[UpgradeManager.O2Level];
    }
    private void Update()
    {
        O2State(); // 산소 상태
        Test();

        // 인벤토리UI
        if (Input.GetKeyUp(KeyCode.I))
        {
            UIManager.InventoryUI.gameObject.SetActive(true);
        }

        // 우주선 부스터
        if (Input.GetKey(KeyCode.Space) && BoosterTimer > 0 && isFlying)
        {
            if (UpgradeManager.engineBreakdown == true || UpgradeManager.wallBreakdown == true || UpgradeManager.O2Breakdown == true) return; // 고장 여부
            BoosterTimer -= Time.deltaTime; // 감소
            boosterEffect.gameObject.SetActive(true);
            spaceshipSpeed = UpgradeManager.wall[UpgradeManager.wallLevel] + UpgradeManager.engine[UpgradeManager.engineLevel];
        }
        else
        {
            boosterEffect.gameObject.SetActive(false);
            spaceshipSpeed = UpgradeManager.engine[UpgradeManager.engineLevel];
            if (BoosterTimer <= 7f && !Input.GetKey(KeyCode.Space))
            {
                BoosterTimer += Time.deltaTime;
            }
        }
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UpgradeManager.O2Upgrade();
        }
    }

    public void Arrived()
    {
        currentTarget = null;
        UIManager.DEPButton.gameObject.SetActive(true);
        UIManager.ExitButton.gameObject.SetActive(true);
        isFlying = false;
    }

    public void O2State() //산소 상태
    {
        if (isO2)
        {
            O2 -= Time.deltaTime; // 산소 감소

        }
        if (O2 <= 0)
        {
            UIManager.ExitButtons(0);
        }
    }
}
