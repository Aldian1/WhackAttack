using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _NetworkManager : MonoBehaviour {


    public GameObject ConnectionDetails;

    public GameObject player;

    public Transform spawn;
    // Use this for initialization
	void Start () {
        Connect();
	}
	
	void Update()
    {
        ConnectionDetails.GetComponent<Text>().text = "Connection Details: " + PhotonNetwork.connectionStateDetailed.ToString();

    }


    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");

    }


    public void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();

    }



        void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Joined failed");
        PhotonNetwork.CreateRoom(null);

    }

    void OnJoinedRoom()
    {

        Debug.Log("Joined Room");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Debug.Log("runonce");
       GameObject go = PhotonNetwork.Instantiate("Player", spawn.position, Quaternion.identity,0) as GameObject;

        go.GetComponent<_Player>().enabled = true;
        go.AddComponent<Rigidbody>();
        go.GetComponent<Rigidbody>().freezeRotation = true;

    }
}
