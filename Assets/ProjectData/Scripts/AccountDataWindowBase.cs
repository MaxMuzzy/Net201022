using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    }
}