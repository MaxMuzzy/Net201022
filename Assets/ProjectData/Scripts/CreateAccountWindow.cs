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
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _email,
            Password = _password
        }, result =>
        {
            Debug.Log($"Successful creation: {_username}");
            EnterInGameScene();
        }, error =>
        {
            Debug.LogError($"Failure: {error.ErrorMessage}");
        });
    }
}