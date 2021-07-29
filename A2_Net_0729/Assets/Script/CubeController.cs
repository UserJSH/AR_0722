using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CubeController : MonoBehaviour
{
    [SerializeField] private PhotonView pv; //Æ÷Åæºä

    float move_speed = 2f; // ÀÌµ¿¼Óµµ
    float rot_speed = 300f; //È¸Àü
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        
    }
}
