using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkSmoother : Photon.MonoBehaviour {

    Vector3 realpos;
    Quaternion realrot;
   public string name_;

    public Text nametext;
	// Use this for initialization
	void Start () {
        name_ = this.GetComponentInChildren<Text>().text;
        nametext = this.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if(photonView.isMine)
        { }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realpos, .1F);
            transform.rotation = Quaternion.Lerp(transform.rotation, realrot, .1F);
            nametext.text = name_;
            
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {

            //sending data to others
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext((string)name_);
        }
        else
        {

            //recieving data
            realpos = (Vector3)stream.ReceiveNext();
            realrot = (Quaternion)stream.ReceiveNext();
            name_ = (string)stream.ReceiveNext();

        }
    }
}
