using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class CreateAccountWindow : AccountDataWindowBase
{
    [SerializeField]
    private TMP_InputField _emailField;

    [SerializeField]
    private Button _createAccountButton;

    private string _email;

    [SerializeField]
    protected TMP_Text _loadingText;

    protected override void SubscribeUIElements()
    {
        base.SubscribeUIElements();

        _emailField.onValueChanged.AddListener(UpdateEmail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void UpdateEmail(string email)
    {
        _email = email;
    }

    private void CreateAccount()
    {
        _loadingText.enabled = true;
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _email,
            Password = _password
        }, result =>
        {
            _loadingText.enabled = false;
            Debug.Log($"Successful creation: {_username}");
            EnterInGameScene();
        }, error =>
        {
            Debug.LogError($"Failure: {error.ErrorMessage}");
        });
    }
}