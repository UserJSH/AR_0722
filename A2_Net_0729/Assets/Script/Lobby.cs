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
        if (GUILayout.Button("룸 생성"))
        {
            string roomname = "Room" + Random.Range(1000, 10000);

            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8; // 최대 인원
            options.IsOpen = true; // 입장 가능 여부
            options.IsVisible = true; // 로비에 룸 노출 여부

            //커스텀 세팅
            options.CustomRoomProperties = new Hashtable();
            options.CustomRoomProperties.Add("Map", 1);
            options.CustomRoomProperties.Add("Color", "red");

            PhotonNetwork.CreateRoom(roomname, options, null);
        }
        if (GUILayout.Button("룸 입장"))
        {
            PhotonNetwork.JoinRandomRoom();
        }
        if (GUILayout.Button("룸 속성 변경"))
        {
            //방장인지 체크
            if (PhotonNetwork.IsMasterClient)
            {
                //룸 내부인지
                if (PhotonNetwork.InRoom)
                {
                    //PhotonNetwork.CurrentRoom.MaxPlayers = 4;
                    //현재 방의 커스텀 정보를 불러옴
                    Hashtable room = PhotonNetwork.CurrentRoom.CustomProperties;
                    room["Map"] = 2;
                    //커스텀 정보를 저장
                    PhotonNetwork.CurrentRoom.SetCustomProperties(room);
                }
               
            }          
        }
        if (GUILayout.Button("플레이어 속성 변경"))
        {
            Hashtable player = PhotonNetwork.LocalPlayer.CustomProperties;
            player["Level"] = 2;
            PhotonNetwork.LocalPlayer.SetCustomProperties(player);
        }
        if (GUILayout.Button("게임 시작"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("GameScenes");
            }
        }
        if (GUILayout.Button("로비 조인"))
        {
            PhotonNetwork.JoinLobby();
        }
        if (GUILayout.Button("로비 나가기"))
        {
            PhotonNetwork.LeaveLobby();
        }
    }


    #region 콜백
    //룸생성
    public override void OnCreatedRoom()
    {
        print("OnCreatedRoom");
    }
    //룸생성 실패
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("OnCreatedRoomFailed " + returnCode + " " + message);
    }
    //룸에 들어갔을 때
    public override void OnJoinedRoom()
    {
        print("OnJoinedRoom");
    }
    //룸에 못들어 갔을 떄
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("OnJoinRoomFailed " + returnCode + " " + message);
    }
    //룸에 랜덤으로 못 들어갔을 때
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("OnJoinRandomFailed " + returnCode + " " + message);
    }
    //룸에서 나갔을 때
    public override void OnLeftRoom()
    {
        print("OnLeftRoom");
    }
    //다른 플레이어가 룸에 들어왔을 때
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("OnPlayerEnteredRoom " + newPlayer.NickName);
    }
    //다른 플레이어가 룸에서 나갔을 때
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("OnPlayerLeftRoom " + otherPlayer.NickName);
    }
    //로비에 들어옴
    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");
    }
    //로비에서 나감
    public override void OnLeftLobby()
    {
        print("OnLeftLobby");
    }
    //룸 속성 변경
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        print("OnRoomPropertiesUpdate " + propertiesThatChanged["Map"]);
    }
    //플레이어 속성 변경
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        print("OnPlayerPropertiesUpdate " + targetPlayer.NickName + " " + changedProps["Level"]);
    }
    //룸정보 리스트
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

