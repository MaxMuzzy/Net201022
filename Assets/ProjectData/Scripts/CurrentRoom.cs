using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class CurrentRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Canvas _roomCanvas;

    [SerializeField]
    private TMP_Text _roomNameText;

    [SerializeField]
    private Toggle _closeRoomToggle;

    [SerializeField]
    private Button _startGameButton;

    // второе задание
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        _roomCanvas.gameObject.SetActive(true);
        var room = PhotonNetwork.CurrentRoom;
        _roomNameText.text = room.Name;
        _closeRoomToggle.onValueChanged.AddListener(_ => room.IsOpen = !_closeRoomToggle.isOn);
        _startGameButton.onClick.AddListener(EnterInGameScene);

    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (!PhotonNetwork.CurrentRoom.IsOpen)
        {
            _startGameButton.interactable = true;
        }
    }
    private void EnterInGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        _roomCanvas.gameObject.SetActive(false);
    }
}
