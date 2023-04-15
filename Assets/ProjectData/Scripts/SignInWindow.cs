using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class SignInWindow : AccountDataWindowBase
{
    [SerializeField]
    private Button _signInButton;

    [SerializeField]
    protected TMP_Text _loadingText;

    protected override void SubscribeUIElements()
    {
        base.SubscribeUIElements();

        _signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        _loadingText.enabled = true;
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password
        }, result =>
        {
            _loadingText.enabled = false;
            Debug.Log($"Successful SignIn: {_username}");
            EnterInGameScene();
        }, error =>
        {
            Debug.LogError($"Failure: {error.ErrorMessage}");
        });
    }
}