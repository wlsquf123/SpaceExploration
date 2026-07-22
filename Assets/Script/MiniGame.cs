using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public GameObject letter; // 글자 맞추기
    public GameObject card; // 카드 게임
    public GameObject MoveBar; // 움직이는 바 맞추기

    [Header("글자 맞추기 게임")]
    public Text RandomletterText; 
    public InputField letterInput;
    public Button letterButton;

    private string[] words = { "바나나", "사과", "옥수수", "딸기", "수박" };
    private string answer;

    public void RandomMiniGames()
    {
        int random = Random.Range(1, 4);

        switch (random)
        {
            case 1: // 글자 맞추기
                letter.SetActive(true);
                answer = words[Random.Range(0, words.Length)];
                RandomletterText.text = answer;
                letterInput.text = "";
                break;
        }
    }

    public void CheckLetter()
    {
        if (letterInput.text == answer)
        {
            Debug.Log("수리 성공!");

            GameManager.Instance.UpgradeManager.RepairSuccess();

            letter.SetActive(false);
        }
        else
        {
            Debug.Log("수리 실패!");

            GameManager.Instance.UpgradeManager.RepairFail();

            letter.SetActive(false);
        }
    }


}
