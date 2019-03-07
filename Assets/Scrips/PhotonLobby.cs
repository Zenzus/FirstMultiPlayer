using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;
    RoomInfo[] rooms;

    public GameObject battleButton;
    public GameObject cancelButton;



    private void Awake()
    {
        lobby = this; // creates singleton , lives witging main menu scene
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connects to master photon server
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the photon master server");
        battleButton.SetActive(true);
    }

    public void onBattleButtonClicked()
    {
        Debug.Log("Battle Button was click");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a radom game but failed. there must be no open games available");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create a new room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("tried to make a new room but failed must be a room with the same name");
        CreateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("we are now in a room");
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("cancel button was clicked");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
