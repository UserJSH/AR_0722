using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Lobby : MonoBehaviourPunCallbacks
{
    private void OnGUI()
    {
        if (GUILayout.Button("�� ����"))
        {
            string roomname = "Room" + Random.Range(1000, 10000);

            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8; // �ִ� �ο�
            options.IsOpen = true; // ���� ���� ����
            options.IsVisible = true; // �κ� �� ���� ����

            //Ŀ���� ����
            options.CustomRoomProperties = new Hashtable();
            options.CustomRoomProperties.Add("Map", 1);
            options.CustomRoomProperties.Add("Color", "red");

            PhotonNetwork.CreateRoom(roomname, options, null);
        }
        if (GUILayout.Button("�� ����"))
        {
            PhotonNetwork.JoinRandomRoom();
        }
        if (GUILayout.Button("�� �Ӽ� ����"))
        {
            //�������� üũ
            if (PhotonNetwork.IsMasterClient)
            {
                //�� ��������
                if (PhotonNetwork.InRoom)
                {
                    //PhotonNetwork.CurrentRoom.MaxPlayers = 4;
                    //���� ���� Ŀ���� ������ �ҷ���
                    Hashtable room = PhotonNetwork.CurrentRoom.CustomProperties;
                    room["Map"] = 2;
                    //Ŀ���� ������ ����
                    PhotonNetwork.CurrentRoom.SetCustomProperties(room);
                }
               
            }          
        }
        if (GUILayout.Button("�÷��̾� �Ӽ� ����"))
        {
            Hashtable player = PhotonNetwork.LocalPlayer.CustomProperties;
            player["Level"] = 2;
            PhotonNetwork.LocalPlayer.SetCustomProperties(player);
        }
        if (GUILayout.Button("���� ����"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("GameScenes");
            }
        }
        if (GUILayout.Button("�κ� ����"))
        {
            PhotonNetwork.JoinLobby();
        }
        if (GUILayout.Button("�κ� ������"))
        {
            PhotonNetwork.LeaveLobby();
        }
    }


    #region �ݹ�
    //�����
    public override void OnCreatedRoom()
    {
        print("OnCreatedRoom");
    }
    //����� ����
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("OnCreatedRoomFailed " + returnCode + " " + message);
    }
    //�뿡 ���� ��
    public override void OnJoinedRoom()
    {
        print("OnJoinedRoom");
    }
    //�뿡 ����� ���� ��
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("OnJoinRoomFailed " + returnCode + " " + message);
    }
    //�뿡 �������� �� ���� ��
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("OnJoinRandomFailed " + returnCode + " " + message);
    }
    //�뿡�� ������ ��
    public override void OnLeftRoom()
    {
        print("OnLeftRoom");
    }
    //�ٸ� �÷��̾ �뿡 ������ ��
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("OnPlayerEnteredRoom " + newPlayer.NickName);
    }
    //�ٸ� �÷��̾ �뿡�� ������ ��
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("OnPlayerLeftRoom " + otherPlayer.NickName);
    }
    //�κ� ����
    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");
    }
    //�κ񿡼� ����
    public override void OnLeftLobby()
    {
        print("OnLeftLobby");
    }
    //�� �Ӽ� ����
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        print("OnRoomPropertiesUpdate " + propertiesThatChanged["Map"]);
    }
    //�÷��̾� �Ӽ� ����
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        print("OnPlayerPropertiesUpdate " + targetPlayer.NickName + " " + changedProps["Level"]);
    }
    //������ ����Ʈ
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("OnRoomListUpdate");

        foreach (var item in roomList)
        {
            print(item.Name);
        }
    }
    #endregion
}

