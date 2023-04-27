using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Linq;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleLabel;

    [SerializeField]
    private GameObject _newCharacterPanel;

    [SerializeField]
    private Button _createCharacterButton;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private List<SlotCharacterWidget> _slots;

    private string _characterName;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);

        GetCharacters();
        foreach (var slot in _slots)
            slot.SlotButton.onClick.AddListener(() => CreateNewCharacterPanelShow(true));
        _inputField.onValueChanged.AddListener(OnNameChanged);
        _createCharacterButton.onClick.AddListener(CreateCharacter);
    }

    private void CreateCharacter()
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
        {
            CharacterName = _characterName,
            ItemId = "CharacterToken"
        }, result => 
        {
            UpdateCharacterStatistics(result.CharacterId);
        }, OnError);
    }

    private void UpdateCharacterStatistics(string characterId)
    {
        PlayFabClientAPI.UpdateCharacterStatistics(new UpdateCharacterStatisticsRequest
        {
            CharacterId = characterId,
            // второе задание
            CharacterStatistics = new Dictionary<string, int>
            {
                {"Health", 100},
                {"Damage", 25 },
                {"XP", 0 }
            }
        }, result =>
        {
            Debug.Log("Successful character creation!");
            CreateNewCharacterPanelShow(false);
            GetCharacters();
        }, OnError);
    }

    private void OnNameChanged(string changedName)
    {
        _characterName = changedName;
    }

    private void CreateNewCharacterPanelShow(bool v)
    {
        _newCharacterPanel.SetActive(v);
    }

    private void GetCharacters()
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(), 
            result =>
            {
                Debug.Log($"Character count: { result.Characters.Count}" );
                ShowCharactersInSlot(result.Characters);
            }, OnError);
    }

    private void ShowCharactersInSlot(List<CharacterResult> characters)
    {
        if (characters.Count == 0)
        {
            foreach (var slot in _slots)
            {
                slot.ShowEmptySlot();
            }
        }
        else if (characters.Count > 0 && characters.Count <= _slots.Count)
        {
            PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest
            {
                CharacterId = characters.First().CharacterId
            },
            result =>
            {
                var health = result.CharacterStatistics["Health"].ToString();
                var damage = result.CharacterStatistics["Damage"].ToString();
                var xp = result.CharacterStatistics["XP"].ToString();

                _slots.First().ShowInfoCharacterSlot(characters.First().CharacterName, health, damage, xp);
            }, OnError);
        }
        else
        {
            Debug.LogError("Add slots for characters.");
        }
    }

    private void OnGetAccount(GetAccountInfoResult result)
    {
        _titleLabel.text = $"PlayFab ID: {result.AccountInfo.PlayFabId}" + "\n" +
            $"Username: {result.AccountInfo.Username}";
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error);
    }
}
