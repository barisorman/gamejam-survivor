using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private Button _card1;
    [SerializeField] private Button _card2;
    [SerializeField] private Button _card3;

    private UpgradeData[] _currentUpgrades;
    private Action<UpgradeData> _onSelected;

    public void Show(UpgradeData[] upgrades, Action<UpgradeData> onSelected)
    {
        _currentUpgrades = upgrades;
        _onSelected = onSelected;
        gameObject.SetActive(true);

        SetupCard(_card1, upgrades[0]);
        SetupCard(_card2, upgrades[1]);
        SetupCard(_card3, upgrades[2]);
    }

    private void SetupCard(Button card, UpgradeData data)
    {
        TMP_Text text = card.GetComponentInChildren<TMP_Text>();
        text.text = data.DisplayName + "\n" + data.Description;

        card.onClick.RemoveAllListeners();
        card.onClick.AddListener(() => OnCardClicked(data));
    }

    private void OnCardClicked(UpgradeData data)
    {
        gameObject.SetActive(false);
        _onSelected?.Invoke(data);
    }
}