using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Canvas _lobbyCanvas;

    [SerializeField]
    private GameObject _roomDisplayPrefab;

    [SerializeField]
    private Transform _parent;

    private List<GameObject> _roomDisplays = new();

    [SerializeField]
    private Button _createRoomButton;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
        _createRoomButton.onClick.AddListener(() => CreateRoom("TestRoom"));
    }
    private void OnGetAccount(GetAccountInfoResult result)
    {
        PhotonNetwork.AuthValues = new AuthenticationValues(result.AccountInfo.PlayFabId);
        PhotonNetwork.NickName = result.AccountInfo.PlayFabId;
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

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby(new TypedLobby("TestLobby", LobbyType.Default));
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        _lobbyCanvas.gameObject.SetActive(true);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach (var display in _roomDisplays)
        {
            if (_roomDisplays.Count != 0)
            {
                Destroy(display);
                _roomDisplays.Remove(display);
            }
        }
        foreach (var room in roomList)
        {
            //первое задание
            var display = Instantiate(_roomDisplayPrefab, _parent);
            _roomDisplays.Add(display);
            display.GetComponentInChildren<TMP_Text>().text = room.Name;
            display.GetComponentInChildren<Button>().onClick.AddListener(() => ConnectToRoom(room.Name));
        }
    }

    private void ConnectToRoom(string roomName) 
    {
        Debug.Log($"Joining room: {roomName}");
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        _lobbyCanvas.gameObject.SetActive(false);
    }

    // третий пункт
    private void CreateRoomForFriends(string roomName, string[] expectedFriendsId)
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions(), new TypedLobby("test", LobbyType.Default), expectedFriendsId);
    }


    // метод для демострации 6-ой домашки
    private void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4, IsVisible = true});
    }
}
