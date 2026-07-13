using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int iron = 2; // 철 ( 초기자원 2개 )
    public int copper = 2; // 구리 
    public int plastic = 2; // 플라스틱
    public int core; // 코어

    public void AddItem(Items ItemType, int add) // 자원을 인벤토리에 넣기
    {
        if (ItemType == Items.iron)
        {
            iron += add;
        }
        if (ItemType == Items.copper)
        {
            copper += add;
        }
        if (ItemType == Items.plastic)
        {
            plastic += add;
        }
        if (ItemType == Items.core)
        {
            core += add;
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
