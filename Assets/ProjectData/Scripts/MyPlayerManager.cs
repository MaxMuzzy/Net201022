using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class MyPlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private float _playerSpeed;

    public static GameObject LocalPlayerInstance;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private int _health;
    private string _playFabId;
    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            _rigidbody = GetComponent<Rigidbody>();

            // третий пункт дз
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
            GetUserDataHealth(_playFabId);
        }
    }

    private void Update()
    {
        // тест чтобы показать синхронизацию игроков
       if (photonView.IsMine)
       {
            _moveDirection = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
            _rigidbody.AddForce(_moveDirection * _playerSpeed * Time.deltaTime * 100f);
       }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    private void GetUserDataHealth(string playFabId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = playFabId
        }, result =>
        {
            if (result.Data.TryGetValue("Health", out var dataHealth))
            {
                _health = int.Parse(dataHealth.Value);
                Debug.Log(_health);
            }
        }, OnError);
    }

    private void OnGetAccount(GetAccountInfoResult result)
    {
        _playFabId = result.AccountInfo.PlayFabId;
    }

    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
    }
}
