using UnityEngine;
using UnityEngine.UI;

public class EnterInGameWindow : MonoBehaviour
{
    [SerializeField]
    private Button _signInButton;

    [SerializeField]
    private Button _createAccountButton;

    [SerializeField]
    private Canvas _enterInGameCanvas;

    [SerializeField]
    private Canvas _signInCanvas;

    [SerializeField]
    private Canvas _createAccountCanvas;

    private void Start()
    {
        _signInButton.onClick.AddListener(OpenSignInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
    }

    private void OpenSignInWindow()
    {
        _signInCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }

    private void OpenCreateAccountWindow()
    {
        _createAccountCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }
}