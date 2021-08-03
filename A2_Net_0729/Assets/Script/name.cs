using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class name : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Transform cam;
    private PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        nameText.text = pv.Owner.NickName;
        if (pv.IsMine)
        {
            nameText.color = Color.green;
        }
        else
        {
            nameText.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ºôº¸µå
        cam.forward = Camera.main.transform.forward;
    }
}
