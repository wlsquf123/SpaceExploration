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
                    float r = Random.value * 100; // 자원의 개수를 구하는 확률 

                    int add = 0; // 실제로 획득할 개수

                    // 우주복 Lv1
                    if (GameManager.Instance.UpgradeManager.suitLevel == 1)
                    {
                        if (Inventory.Limit(item.ItemType, 1))
                        {
                            add = 1;
                        }
                    }

                    // 우주복 Lv2
                    else if (GameManager.Instance.UpgradeManager.suitLevel == 2)
                    {
                        // 20% 확률로 2개 획득 시도
                        if (r <= 20f)
                        {
                            if (Inventory.Limit(item.ItemType, 2))
                            {
                                add = 2;
                            }
                            else if (Inventory.Limit(item.ItemType, 1))
                            {
                                add = 1;
                            }
                        }
                        else
                        {
                            if (Inventory.Limit(item.ItemType, 1))
                            {
                                add = 1;
                            }
                        }
                    }

                    // 우주복 Lv3
                    else if (GameManager.Instance.UpgradeManager.suitLevel == 3)
                    {
                        // 15% 확률로 3개 획득 시도
                        if (r <= 15f)
                        {
                            if (Inventory.Limit(item.ItemType, 3))
                            {
                                add = 3;
                            }
                            else if (Inventory.Limit(item.ItemType, 2))
                            {
                                add = 2;
                            }
                            else if (Inventory.Limit(item.ItemType, 1))
                            {
                                add = 1;
                            }
                        }

                        // 30% 확률로 2개 획득 시도
                        else if (r <= 45f)
                        {
                            if (Inventory.Limit(item.ItemType, 2))
                            {
                                add = 2;
                            }
                            else if (Inventory.Limit(item.ItemType, 1))
                            {
                                add = 1;
                            }
                        }

                        // 나머지는 1개 획득 시도
                        else
                        {
                            if (Inventory.Limit(item.ItemType, 1))
                            {
                                add = 1;
                            }
                        }
                    }

                    // 한 개 이상 들어갈 공간이 있을 때만 획득
                    if (add > 0)
                    {
                        Inventory.AddItem(item.ItemType, item.LevelType, add);

                        Debug.Log(add + "개 획득");

                        currentItem = null;
                        Destroy(item.gameObject);
                    }
                    else
                    {
                        Debug.Log("인벤토리 무게가 가득 찼습니다.");
                    }
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
