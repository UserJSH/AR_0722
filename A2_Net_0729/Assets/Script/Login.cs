using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Login : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // 방장의 Scenes에 따라 이동
        string nickname = "Player" + Random.Range(1000, 10000);

        PhotonNetwork.LocalPlayer.NickName = nickname; //닉네임 설정

        //플레이어 속성
        Hashtable player = new Hashtable
        {
            {"Level", 1 },{"Exp", 0 },{"Color", Random.Range(0, 7)}
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(player);

        Connect();
        
    }

    
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    
    // 네임서버 연결 성공
    #region 콜백
    public override void OnConnected()
    {
        print("OnConnected");
    }
    // 마스터 서버 연결 성공
    public override void OnConnectedToMaster()
    {
        print("OnConnectedToMaster");

        PhotonNetwork.LoadLevel("LobbyScenes");
    }
    //서버연결 끊김
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("OnDisConnted");
    }
    #endregion
}
