using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;
using Photon.Realtime;
public class CustomLagCompensation : MonoBehaviour, IPunObservable
{
    Rigidbody2D rb;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rb.position);
            //stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else
        {
            rb.position = (Vector2)stream.ReceiveNext();
            //rb.rotation = (Quaternion)stream.ReceiveNext();
            rb.velocity = (Vector2)stream.ReceiveNext();

            float lag = Mathf.Abs((float) (PhotonNetwork.time - info.timestamp));
            rb.position += (rb.velocity * lag);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
