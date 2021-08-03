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
        //오브제 생성
        PhotonNetwork.Instantiate(cube.name, cube.position, Quaternion.identity, 0); //Quaternion.identity => 회전을 하지 않는 상태
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //방장이 바뀜
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        print("OnMasterClientSwitched " + newMasterClient.NickName);
    }
}
