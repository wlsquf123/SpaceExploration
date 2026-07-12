using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public UIManager TargetUIManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        var subCam = GameObject.FindGameObjectWithTag("SubCamera");
        var player = GameObject.FindGameObjectWithTag("Player");

        TargetUIManager.MainCamera = mainCam;
        TargetUIManager.SubCamera = subCam;
        TargetUIManager.PlayerOBJ = player;

        if (player)
            player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
