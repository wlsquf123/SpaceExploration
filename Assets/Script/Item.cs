using UnityEngine;

public enum Items
{
    None,
    iron, // Ã¶ Ã¶ Ã¶ Ã¶ ÀÌ°Åµµ Áß±¹¾î µÇ³ª
    copper, // ±¸¸®
    plastic, // ÇÃ¶ó½ºÆ½
    core // ÄÚ¾î
    
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

    public GameObject isLight; // ºû È°¼ºÈ­

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
