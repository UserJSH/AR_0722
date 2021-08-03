using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class CubeController : MonoBehaviour, IPunObservable
{
    [SerializeField] private PhotonView pv; //포톤뷰
    [SerializeField] private Transform tr;
    private Rigidbody rigid;

    Vector3 currPos = Vector3.zero;
    Quaternion currRot = Quaternion.identity;

    Vector3 currRigidPos = Vector3.zero;
    Quaternion currRigidRot = Quaternion.identity;

    Vector3 currVel = Vector3.zero;

    float move_speed = 2f; // 속도
    float rot_speed = 300f; // 회전

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //보내는 Stream
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);

            stream.SendNext(rigid.position);
            stream.SendNext(rigid.rotation);
            stream.SendNext(rigid.velocity);
        }
        else
        {
            //받는 Stream
            if (!pv.IsMine)
            {
                currPos = (Vector3)stream.ReceiveNext();
                currRot = (Quaternion)stream.ReceiveNext();

                currRigidPos = (Vector3)stream.ReceiveNext();
                currRigidRot = (Quaternion)stream.ReceiveNext();
                currVel = (Vector3)stream.ReceiveNext();

                //float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
                //GetComponent<Rigidbody>().position += GetComponent<Rigidbody>().velocity * lag;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currPos = tr.position;
        currRot = tr.rotation;

        rigid = GetComponent<Rigidbody>();
        currVel = rigid.velocity;
        currRigidPos = rigid.position;
        currRigidRot = rigid.rotation;

        pv.RPC("ColorSetup", RpcTarget.All);
        //this.GetComponent<Renderer>().material.color = GetColor(int.Parse(PhotonNetwork.LocalPlayer.CustomProperties["Color"].ToString()));
    }

    [PunRPC]
    void ColorSetup()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");

        foreach (var p in PhotonNetwork.PlayerList)
        {
            foreach (var o in obj)
            {
                if (p.ActorNumber == Convert.ToInt32(o.GetPhotonView().ViewID / 1000))
                {
                    o.GetComponent<Renderer>().material.color = GetColor(Convert.ToInt32(p.CustomProperties["Color"]));
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * move_speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * Time.deltaTime * move_speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.down * Time.deltaTime * rot_speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * rot_speed);
            }
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10f);
            tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 10f);

            rigid.position = Vector3.MoveTowards(rigid.position, currRigidPos, Time.fixedDeltaTime * 10f);
            rigid.rotation = Quaternion.RotateTowards(rigid.rotation, currRigidRot, Time.fixedDeltaTime * 10f); 
            rigid.velocity = currVel;
        }
        
    }
    public static Color GetColor(int color)
    {
        switch (color)
        {
            case 0: return Color.red;
            case 1: return Color.magenta;
            case 2: return Color.yellow;
            case 3: return Color.green;
            case 4: return Color.blue;
            case 5: return Color.cyan;
            case 6: return Color.grey;
        }

        return Color.black;
    }
}
