using UnityEngine;
using UnityEngine.UI;

public class ConfigLugar : MonoBehaviour
{
    public Player player;
    public Image[] slots;
    public int ID;

    public void Start()
    {
        for(int i = 0; i<4; i++)
        {
            if(i == PlayerPrefs.GetInt("CurrentSlot",2))
            {
                slots[i].color = Color.white;
            }
            else
                slots[i].color = Color.grey;
        }
    }
    public void SetslotX(int X)
    {
        PlayerPrefs.SetInt("CurrentSlot",ID);
        PlayerPrefs.SetInt("PosX",X);
        player.setMouseDistancePos();
    }
     public void SetslotY(int Y)
    {
        PlayerPrefs.SetInt("PosY",Y);
        player.setMouseDistancePos();
    }
    public void Slots()
    {
        for(int i = 0; i<4; i++)
        {
            if(i == PlayerPrefs.GetInt("CurrentSlot",2))
            {
                slots[i].color = Color.white;
            }
            else
                slots[i].color = Color.grey;
        }
    }
}