using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Com.TowerDash.TowerDash
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {
        #region MONOBEHAVIOUR MESSAGES


        // Use this for initialization
        void Start()
        {    


        }


        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }
        }

        #endregion
    }
}