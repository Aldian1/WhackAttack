using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkSmoother : Photon.MonoBehaviour {

    Vector3 realpos;
    Quaternion realrot;
   public string name_;

    public Transform childsprite;
    public Text nametext;
	// Use this for initialization
	void Start () {
        name_ = this.GetComponentInChildren<Text>().text;
        nametext = this.GetComponentInChildren<Text>();
        childsprite = transform.GetChild(0);

    }
	
	// Update is called once per frame
	void Update () {
        if(photonView.isMine)
        { }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realpos, .1F);
            childsprite.rotation = Quaternion.Lerp(childsprite.rotation, realrot, .1F);
            nametext.text = name_;
            
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {

            //sending data to others
            stream.SendNext(transform.position);
            stream.SendNext(childsprite.rotation);
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
