using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeData[] _allUpgrades;
    [SerializeField] private UpgradePanel _upgradePanel;

    private PlayerStats _playerStats;
    private PlayerWeapon _playerWeapon;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerStats = player.GetComponent<PlayerStats>();
            _playerWeapon = player.GetComponent<PlayerWeapon>();
        }
    }

    public void ShowUpgradeCards()
    {
        // Pick 3 random unique upgrades
        List<UpgradeData> shuffled = new List<UpgradeData>(_allUpgrades);
        for (int i = 0; i < shuffled.Count; i++)
        {
            int rand = Random.Range(i, shuffled.Count);
            UpgradeData temp = shuffled[i];
            shuffled[i] = shuffled[rand];
            shuffled[rand] = temp;
        }

        UpgradeData[] picks = new UpgradeData[] 
        { 
            shuffled[0], 
            shuffled[1], 
            shuffled[2] 
        };

        _upgradePanel.Show(picks, ApplyUpgrade);
    }

    public void ApplyEvolvedUpgrade()
    {
        // For now just show cards — evolved upgrades can be added later
        ShowUpgradeCards();
    }

    public void ApplyUpgrade(UpgradeData data)
    {
        if (data.Type == UpgradeType.MaxHP)
        {
            _playerStats.IncreaseMaxHp(data.Value);
        }
        else if (data.Type == UpgradeType.MoveSpeed)
        {
            _playerStats.IncreaseMoveSpeed(data.Value);
        }
        else if (data.Type == UpgradeType.MagnetRange)
        {
            _playerStats.IncreaseMagnetRange(data.Value);
        }
        else if (data.Type == UpgradeType.ProjectileDamage)
        {
            _playerStats.IncreaseDamage(data.Value);
        }
        else if (data.Type == UpgradeType.ProjectileFireRate)
        {
            _playerWeapon.IncreaseFireRate(data.Value);
        }
        else if (data.Type == UpgradeType.ProjectileSpeed)
        {
            _playerWeapon.IncreaseProjSpeed(data.Value);
        }

        GameManager.Instance.SetPaused(false);
    }
}