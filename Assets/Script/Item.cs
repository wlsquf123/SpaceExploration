using UnityEngine;

public enum Items
{
    None,
    iron, // 枚
    copper, // 备府
    plastic, // 敲扼胶平
    core // 内绢
}

public enum Levels
{
    None,
    Lv1 = 1,
    Lv2 = 2,
    Lv3 = 3,
}

public class Item : MonoBehaviour
{
    public Levels LevelType;
    public Items ItemType; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
