using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotCharacterWidget : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private GameObject _emptySlot;

    [SerializeField]
    private GameObject _infoCharacterSlot;

    [SerializeField]
    private TMP_Text _nameLabel;

    [SerializeField]
    private TMP_Text _healthLabel;

    [SerializeField]
    private TMP_Text _damageLabel;

    [SerializeField]
    private TMP_Text _xpLabel;

    public Button SlotButton => _button;

    public void ShowInfoCharacterSlot(string name, string health, string damage, string xp)
    {
        _nameLabel.text = name;
        _healthLabel.text = $"Health: {health}";
        _damageLabel.text = $"Damage: {damage}";
        _xpLabel.text = $"XP: {xp}";

        _emptySlot.SetActive(false);
        _infoCharacterSlot.SetActive(true);
    }

    public void ShowEmptySlot()
    {
        _infoCharacterSlot.SetActive(false);
        _emptySlot.SetActive(true);
    }
}
