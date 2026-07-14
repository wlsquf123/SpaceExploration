using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Camera playerCamera; // 플레이어 카메라

    public Inventory Inventory; // 인벤토리

    private Item currentItem; // 현재 아이템

    void Start()
    {
        Inventory = GameManager.Instance.Inventory;
    }

    void Update()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            Item item = hit.collider.GetComponentInParent<Item>();

            if (item != null)
            {
                if (currentItem != item)
                {
                    currentItem = item;
                    currentItem.isLight.SetActive(true);
                }

                // E키로 자원 수집
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Inventory.AddItem(item.ItemType, item.LevelType, 1);
                    currentItem = null;

                    Destroy(item.gameObject);
                }
                return;
            }
        }

        // 자원을 바라보지 않는 경우
        if (currentItem != null)
        {
            currentItem.isLight.SetActive(false);
            currentItem = null;
        }
    }
}
