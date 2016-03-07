using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkSmoother : Photon.MonoBehaviour {

    Vector3 realpos;
    Quaternion realrot;
   public string name_;

    public Transform childsprite;

    public GameObject face, boomerang;
    public Text nametext;

    public Sprite head_;
    public Sprite boomerang_;

    private Sprite[] otherhead;

    public int headnum;


  
    // Use this for initialization
    void Start () {

        //getting any components that we need
      
        name_ = this.GetComponentInChildren<Text>().text;
        nametext = this.GetComponentInChildren<Text>();
        childsprite = transform.GetChild(1);
        face = this.transform.FindChild("Sprite").gameObject;
        boomerang = this.transform.GetChild(1).FindChild("Boomerang").gameObject;
        face.GetComponent<SpriteRenderer>().sprite = head_;
        //getting sprite list
        otherhead = GameObject.Find("Main Camera").GetComponent<_NetworkManager>().Heads;
    }
	
	// Update is called once per frame
	void Update () {
        if(photonView.isMine)
        { }
        else
        {

            //if the photon view isnt ours we update the client computers copys with the details they need including pos and skins/name
            transform.position = Vector3.Lerp(transform.position, realpos, .1F);
            childsprite.rotation = Quaternion.Lerp(childsprite.rotation, realrot, .1F);
            nametext.text = name_;
            face.GetComponent<SpriteRenderer>().sprite = otherhead[headnum];
              
            
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
            stream.SendNext((int)headnum);
        }
        else
        {

            //recieving data
            realpos = (Vector3)stream.ReceiveNext();
            realrot = (Quaternion)stream.ReceiveNext();
            name_ = (string)stream.ReceiveNext();
            headnum = (int)stream.ReceiveNext();

        }
    }
}
