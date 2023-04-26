using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class AccountDataWindowBase : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _usernameField;

    [SerializeField]
    private TMP_InputField _passwordField;

    protected string _username;
    protected string _password;

    private void Start()
    {
        SubscribeUIElements();
    }

    protected virtual void SubscribeUIElements()
    {
        _usernameField.onValueChanged.AddListener(UpdateUsername);
        _passwordField.onValueChanged.AddListener(UpdatePassword);
    }

    private void UpdateUsername(string username)
    {
        _username = username;
    }

    private void UpdatePassword(string password)
    {
        _password = password;
    }

    protected void EnterInGameScene()
    {
        SceneManager.LoadScene(1);
        //третий пункт дз
        SetUserData();
    }

    private void SetUserData() 
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Health", "100" }
            }
        },
        result =>
        {
            Debug.Log("SetUserData");
        }, OnError);
    }

    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
    }
}