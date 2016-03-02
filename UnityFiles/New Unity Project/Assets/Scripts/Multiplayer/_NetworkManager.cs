using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _NetworkManager : MonoBehaviour {


    public GameObject ConnectionDetails;

    public GameObject loginscreen;

    public GameObject player;

    public Transform spawn;

    public string PlayersName;
    // Use this for initialization
	void Start () {
      
	}
	
	void Update()
    {
        ConnectionDetails.GetComponent<Text>().text = "Connection Details: " + PhotonNetwork.connectionStateDetailed.ToString();

    }


    public void Connect(Text PlayerName)
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PlayersName = PlayerName.text;
        loginscreen.SetActive(false);
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
        go.GetComponentInChildren<Text>().text = PlayersName;

    }
}
