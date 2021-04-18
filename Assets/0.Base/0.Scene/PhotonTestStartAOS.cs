using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Anvil;
using System.IO;

public class PhotonTestStartAOS : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private UnityEngine.Events.UnityEvent startEvent;

    // Start is called before the first frame update
    public void Start()
    {
        MainSystem.Instance.SystemStart();
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "0.1";
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        startEvent?.Invoke();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null);
    }
}
