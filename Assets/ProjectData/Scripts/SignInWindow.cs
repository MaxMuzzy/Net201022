using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class SignInWindow : AccountDataWindowBase
{
    [SerializeField]
    private Button _signInButton;

    protected override void SubscribeUIElements()
    {
        base.SubscribeUIElements();

        _signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password
        }, result =>
        {
            Debug.Log($"Successful SignIn: {_username}");
            EnterInGameScene();
        }, error =>
        {
            Debug.LogError($"Failure: {error.ErrorMessage}");
        });
    }
}