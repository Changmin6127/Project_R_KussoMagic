using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PhotonTestStart : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private UnityEngine.Events.UnityEvent startEvent;
    [SerializeField]
    private GameObject playerPrefab;

    // Start is called before the first frame update
    public void Initialize()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "0.1";
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        float randomX = Random.Range(-6f, 6f);

        StartCoroutine(StartCo());
    }
    IEnumerator StartCo()
    {
        yield return new WaitForSeconds(2);
        startEvent?.Invoke();
        PhotonNetwork.PrefabPool.RegisterPrefab("player", playerPrefab);
        PhotonNetwork.Instantiate("player", new Vector3(26.4f, 0f, 24.4f), Quaternion.identity, 0);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null);
    }
}
