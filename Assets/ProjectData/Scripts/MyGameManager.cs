using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab;

    static public MyGameManager Instance;

    private void Start()
    {
        Instance = this;

        if (MyPlayerManager.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, Random.Range(1, 5), 0f), Quaternion.identity, 0);
        }
    }
}
