using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int[] iron = { 2, 0, 0 }; // 철 ( 초기자원 2개 )

    public int[] copper = { 2, 0, 0 }; // 구리

    public int[] plastic = { 2, 0, 0 }; // 플라스틱

    public int[] core = { 0, 0 }; // 코어

    public int InventoryKg() // 인벤토리 무게 계산
    {
        int hap = 0;

        // 철
        hap += iron[0] * 1;
        hap += iron[1] * 2;
        hap += iron[2] * 3;

        // 구리
        hap += copper[0] * 1;
        hap += copper[1] * 2;
        hap += copper[2] * 3;

        // 플라스틱
        hap += plastic[0] * 1;
        hap += plastic[1] * 2;
        hap += plastic[2] * 3;

        // 코어
        hap += core[0] * 4;
        hap += core[1] * 6;

        return hap;
    }

    public bool Limit(Items itemstype, int num)
    {
        // 인벤토리 무게 메개변수 아이템 비교 두개일때 100이면 빠꾸 리턴) 하나를 더했을때 100ㅇ 안넘으면 하나만 추가
        if (InventoryKg() >= GameManager.Instance.UpgradeManager.GetSuit())
        {
            return false;
        }
        if (InventoryKg() + num >= GameManager.Instance.UpgradeManager.GetSuit())
        {
            return false;
        }

        return true;
    }

    public void AddItem(Items ItemType, Levels LvType, int add) // 어떤 자원의 어떤 레벨을 몇 개 추가할지 받는다.
    {
        int level = (int)LvType - 1;

        // 철
        if (ItemType == Items.iron)
        {
            iron[level] += add;
        }
        
        // 구리
        if (ItemType == Items.copper)
        {
            copper[level] += add;
        }

        // 플라스틱
        if (ItemType == Items.plastic)
        {
            plastic[level] += add;
        }

        // 코어
        if (ItemType == Items.core)
        {
            core[level] += add;
        }
    }

    public void grgong(int index)
    {
        // 철 Lv1 가공 
        if (index == 1 && iron[0] >= 2) 
        {
            iron[0] -= 2;
            iron[1] += 1;
        }

        // 구리 Lv1 가공
        if (index == 2 && copper[0] >= 2)
        {
            copper[0] -= 2;
            copper[1] += 1;
        }

        // 플라스틱 lv1 가공
        if (index == 3 && plastic[0] >= 2)
        {
            plastic[0] -= 2;
            plastic[1] += 1;
        }

        // 철 Lv2 가공 
        if (index == 4 && iron[1] >= 2)
        {
            iron[1] -= 2;
            iron[2] += 1;
        }

        // 구리 Lv2 가공
        if (index == 5 && copper[1] >= 2)
        {
            copper[1] -= 2;
            copper[2] += 1;
        }

        // 플라스틱 lv2 가공
        if (index == 6 && plastic[1] >= 2)
        {
            plastic[1] -= 2;
            plastic[2] += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
