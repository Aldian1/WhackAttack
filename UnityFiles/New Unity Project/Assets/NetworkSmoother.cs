using UnityEngine;
using System.Collections;

public class NetworkSmoother : Photon.MonoBehaviour {

    Vector3 realpos;
    Quaternion realrot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(photonView.isMine)
        { }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realpos, .1F);
            transform.rotation = Quaternion.Lerp(transform.rotation, realrot, .1F);
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realpos = (Vector3)stream.ReceiveNext();
            realrot = (Quaternion)stream.ReceiveNext();

        }
    }
}
