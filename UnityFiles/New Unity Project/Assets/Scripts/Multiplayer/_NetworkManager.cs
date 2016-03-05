using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _NetworkManager : Photon.MonoBehaviour {


    public Text ConnectionDetails;

    public GameObject loginscreen,connectionscreen,mainscreen,bg;

    public GameObject player;

    public Transform spawn;

    public string PlayersName;
    // Use this for initialization
	void Start () {
      
	}
	
	void Update()
    {
      if(PhotonNetwork.connecting)
        {
            ConnectionDetails.text = PhotonNetwork.connectionStateDetailed.ToString();
            

        }

    }


    public void Connect(Text PlayerName)
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PlayersName = PlayerName.text;
        loginscreen.SetActive(false);
        connectionscreen.SetActive(true);
    }


    public void OnConnectedToMaster()
    {
        connectionscreen.SetActive(false);
        mainscreen.SetActive(true);
       

    }

    public void StartQue()
    {
        PhotonNetwork.JoinRandomRoom();
        mainscreen.SetActive(false);
        bg.SetActive(false);
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

        go.GetComponent<PlayerControl>().enabled = true;
        go.AddComponent<Rigidbody>();
        go.GetComponent<Rigidbody>().freezeRotation = true;
        go.GetComponentInChildren<Text>().text = PlayersName;

    }

    public void quit()
    {
        PhotonNetwork.Disconnect();
    }

    void OnDisconnectedFromPhoton()
    {
        Application.Quit();
    }
}
