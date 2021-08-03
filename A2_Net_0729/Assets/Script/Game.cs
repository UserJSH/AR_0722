using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Game : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform cube;

    // Start is called before the first frame update
    void Start()
    {
        //������ ����
        PhotonNetwork.Instantiate(cube.name, cube.position, Quaternion.identity, 0); //Quaternion.identity => ȸ���� ���� �ʴ� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //������ �ٲ�
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        print("OnMasterClientSwitched " + newMasterClient.NickName);
    }
}
