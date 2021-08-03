using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnClickInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject prefab; //½ºÇÇ¾î

    void OnClick()
    {
        if (!PhotonNetwork.InRoom) return;

        PhotonNetwork.Instantiate(prefab.name, InputToEvent.inputHitPos + new Vector3(0, 5f, 0), Quaternion.identity, 0);
    }

}
