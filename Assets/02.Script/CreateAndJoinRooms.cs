using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField JoinInput;
    public InputField NameInput;

    public Text CreateRoomButton;

    private void Update()
    {

        if (PhotonNetwork.CurrentRoom != null)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonNetwork.LoadLevel("Game");
            }
        }
    }

    public void CreateRoom()
    {
        CreateRoomButton.text = "상대방 대기중";
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
      PhotonNetwork.JoinRoom(JoinInput.text);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

}
