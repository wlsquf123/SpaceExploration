using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int[] iron = { 2, 0, 0 }; // 철 ( 초기자원 2개 )

    public int[] copper = { 2, 0, 0 }; // 구리

    public int[] plastic = { 2, 0, 0 }; // 플라스틱

    public int[] core = { 0, 0 }; // 코어

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
