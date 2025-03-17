using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomLagCompensationV2 : Photon.MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 netPosition;
    private Vector2 previousPos;

    public bool tpIfFar;
    public float tpDistance;

    [Header("Lerp Values")]
    public float smoothPos = 5.0f;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //custom written transform view that updates more frequently to track player movement online more precisely.
        if (stream.isWriting)
        {
            stream.SendNext(rb.position);
            //stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else
        {
            netPosition = (Vector2)stream.ReceiveNext();
            //rb.rotation = (Quaternion)stream.ReceiveNext();
            rb.velocity = (Vector2)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.time - info.timestamp));
            netPosition += (rb.velocity * lag);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 60;
    }

    void FixedUpdate()
    {
        if (photonView.isMine) return;

        rb.position = Vector2.Lerp(rb.position, netPosition,  smoothPos * Time.fixedDeltaTime);

        if(Vector2.Distance(rb.position, netPosition) > tpDistance)
        {
            rb.position = netPosition;
        }
  
    }
    // Update is called once per frame
    
}
