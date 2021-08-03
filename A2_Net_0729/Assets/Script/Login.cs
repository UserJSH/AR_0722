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
        PhotonNetwork.AutomaticallySyncScene = true; // ������ Scenes�� ���� �̵�
        string nickname = "Player" + Random.Range(1000, 10000);

        PhotonNetwork.LocalPlayer.NickName = nickname; //�г��� ����

        //�÷��̾� �Ӽ�
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

    
    // ���Ӽ��� ���� ����
    #region �ݹ�
    public override void OnConnected()
    {
        print("OnConnected");
    }
    // ������ ���� ���� ����
    public override void OnConnectedToMaster()
    {
        print("OnConnectedToMaster");

        PhotonNetwork.LoadLevel("LobbyScenes");
    }
    //�������� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("OnDisConnted");
    }
    #endregion
}
