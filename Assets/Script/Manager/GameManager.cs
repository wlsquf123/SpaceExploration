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

    public float spaceshipSpeed; // 속도

    public float MaxO2 = 100f;
    public float O2 = 0; // 산소
    public bool isO2 = false; // 탑승 시 산소 유지 : 하선 시 산소 작동

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
        O2State(); // 산소 상태

        if (Input.GetKeyUp(KeyCode.I))
        {
            UIManager.InventoryOBJ.gameObject.SetActive(!UIManager.InventoryOBJ.activeSelf);
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
            Time.timeScale = 0f;
            UIManager.ExitButtons(0);
        }
    }
}
