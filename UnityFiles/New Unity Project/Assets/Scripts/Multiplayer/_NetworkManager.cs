using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _NetworkManager : Photon.MonoBehaviour {


    public Text ConnectionDetails;

    public GameObject loginscreen,connectionscreen,mainscreen,bg,canvasoverlay;

    public GameObject player;

    public Transform spawn;

    public string PlayersName;

    public Sprite[] Heads;
    public Sprite[] Boomerangs;

    private int currentsprite;

    public Image avatar;
    // Use this for initialization
	void Start () {

        avatar.sprite = Heads[0];

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
        PhotonNetwork.ConnectUsingSettings("0.2");
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
        go.GetComponent<NetworkSmoother>().head_ = Heads[currentsprite];
        go.GetComponent<NetworkSmoother>().headnum = currentsprite;
        canvasoverlay.SetActive(true);
    }

    public void quit()
    {
        PhotonNetwork.Disconnect();
    }

    void OnDisconnectedFromPhoton()
    {
        Application.Quit();
    }

    public void ChangeAvatar(int button)
    {
        //left
        if(button == 0)
        {
            currentsprite -= 1;

        }
        //right
        if (button == 1)
        {
            currentsprite += 1;
        }

        //overflow checker
        if(currentsprite >= Heads.Length)
        {
            currentsprite = 0;
        }else if(currentsprite < 0)
        {
            currentsprite = Heads.Length;
        }

        //set the current sprite
        avatar.sprite = Heads[currentsprite];
    }
}
