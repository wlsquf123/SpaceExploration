using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager UIManager;
    public UpgradeManager UpgradeManager;
    public SceneManage SceneManager;
    public Inventory Inventory;

    public Transform moonObject; // 달 오브젝트

    public Transform currentTarget;     // 현재 목적지

    public float spaceshipSpeed; // 우주선 속도

    public float Spacebooster; // 우주선 부스터
    public bool isSpacebooster = true; // 탑승 부스터 사용 : 하선 부스터 사용안함
    public float Timer = 7f;

    public float MaxO2; // 최대 산소
    public float O2 = 0; // 산소
    public bool isO2 = false; // 탑승 산소 100% 유지 : 하선 산소 작동

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
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
        if (Input.GetKey(KeyCode.Space) && Timer >= 0 && isSpacebooster)
        {
            Timer -= Time.deltaTime;
            spaceshipSpeed = UpgradeManager.wall[UpgradeManager.wallLevel] + UpgradeManager.engine[UpgradeManager.engineLevel];
        }
        else
        {
            spaceshipSpeed = UpgradeManager.engine[UpgradeManager.engineLevel];
            if (!Input.GetKey(KeyCode.Space) && Timer <= 7f)
            {
                Timer += Time.deltaTime;
            }
        }
    }

    public void Test()
    {
        // 산소 테스트
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
