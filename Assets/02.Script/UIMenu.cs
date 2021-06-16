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


    public void MultiPlayClick()//��Ƽ�÷���on 
    {
        UIBundle[(int)UIenumer.Multi].SetActive(true);
        UIBundle[(int)UIenumer.Panel].SetActive(true);
    }
    public void MultiPlayOff()//��Ƽ�÷���off
    {
        UIBundle[(int)UIenumer.Multi].SetActive(false);
        UIBundle[(int)UIenumer.Panel].SetActive(false);
    }
    public void CreateRoomClick() //����� 
    {
        UIBundle[(int)UIenumer.CreateRoom].SetActive(true);
    }
    public void CreateRoomOff()//����� ���� 
    {
        UIBundle[(int)UIenumer.CreateRoom].SetActive(false);
    }
    public void InputRoomClick()//�� ���� 
    {
        UIBundle[(int)UIenumer.InputRoom].SetActive(true);
    }
    public void InputRoomOff()// �� ���� off 
    {
        UIBundle[(int)UIenumer.InputRoom].SetActive(false);
    }
    public void SinglePlayClick() // �̱� on 
    {

    }
    


}
