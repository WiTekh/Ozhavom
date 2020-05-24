using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class ennemyStats : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]public float health;
    public float poison = 0;
    public float dmg = 50;
    private float tick= 25;
    private Animator anim;

    private void Update()
    {
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (health <= 0)
            {
                //PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "items", "coin"), transform.position, Quaternion.identity);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<ennemyBehaviour>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                //PhotonNetwork.Destroy(gameObject);
            }
            else if (tick == 25)
            {
                health -= poison;
                tick = 0;
            }
            else
            {
                tick++;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(poison);
        }
        else
        {
            this.health = (float) stream.ReceiveNext();
            this.poison = (float) stream.ReceiveNext();
        }
    }
}
