using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public enum UIenumer
{
    Panel = 0, 
    Multi = 1, 
    InputRoom = 2, 
    CreateRoom = 3,
}



public class UIMenu : MonoBehaviour
{
    public GameObject[] UIBundle;


    public void MultiPlayClick()//멀티플레이on 
    {
        UIBundle[(int)UIenumer.Multi].SetActive(true);
        UIBundle[(int)UIenumer.Panel].SetActive(true);
    }
    public void MultiPlayOff()//멀티플레이off
    {
        UIBundle[(int)UIenumer.Multi].SetActive(false);
        UIBundle[(int)UIenumer.Panel].SetActive(false);
    }
    public void CreateRoomClick() //방생성 
    {
        UIBundle[(int)UIenumer.CreateRoom].SetActive(true);
    }
    public void CreateRoomOff()//방생성 종료 
    {
        UIBundle[(int)UIenumer.CreateRoom].SetActive(false);
    }
    public void InputRoomClick()//방 참여 
    {
        UIBundle[(int)UIenumer.InputRoom].SetActive(true);
    }
    public void InputRoomOff()// 방 참여 off 
    {
        UIBundle[(int)UIenumer.InputRoom].SetActive(false);
    }
    public void SinglePlayClick() // 싱글 on 
    {

    }
    


}
