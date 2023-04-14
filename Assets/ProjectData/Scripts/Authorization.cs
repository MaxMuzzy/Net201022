using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Authorization : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _playFabTitle;
    [SerializeField] private Button _logInButton;
    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = _playFabTitle;

        _logInButton.onClick.AddListener(Login);
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "TestUser",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                _text.color = Color.green;
                _text.text = "Connection successful!";
                Debug.Log(result.PlayFabId);
                PhotonNetwork.AuthValues = new AuthenticationValues(result.PlayFabId);
                PhotonNetwork.NickName = result.PlayFabId;
                ConnectMP();
            },
            error => 
            {
                _text.color = Color.red;
                _text.text = "Error! See logs for more details.";
                Debug.LogError(error);
            });
    }

    private void ConnectMP()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsConnected)
        {
            return;
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    }
    
    
    // Третий пункт домашки
    // ----
    private void DisconnectMP()
    {
        if (PhotonNetwork.IsConnected)
        {   
            PhotonNetwork.Disconnect();
            Debug.Log("Disconnected!");
        }
    }
    // ----
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");
    }
}
