using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameObjectInfo : MonoBehaviour
{
    private void OnGUI()
    {
        if (InputToEvent.goPointedAt != null)
        {
            PhotonView pv = InputToEvent.goPointedAt.GetPhotonView();

            if (pv != null)
            {
                GUI.Label(new Rect(Input.mousePosition.x + 5, Screen.height - Input.mousePosition.y - 10, 300, 30),
                    string.Format("ViewID {0} {1}", pv.ViewID, (pv.IsMine) ? "mine" : pv.Owner.ToString()));
            }
        }
    }
}
