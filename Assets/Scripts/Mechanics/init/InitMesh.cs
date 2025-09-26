using System;
using TMPro;
using UnityEngine;

public class InitMesh : InitFsm
{

    [Header("UI TextMeshPro")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI jumpKeyText;
    public TextMeshProUGUI dashKeyText;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnCoinsChanged;
    public event Action<int> OnKeysChanged;
    public event Action<int> OnLivesChanged;
    public event Action<string> OnAchievementUnlocked;

    protected override void Start()
    {
        base.Start();
        if (healthText != null) OnHealthChanged += (v) => healthText.text = "Salud: " + v;
        if (coinsText != null) OnCoinsChanged += (v) => coinsText.text = "Monedas: " + v;
        if (livesText != null) OnLivesChanged += (v) => livesText.text = "Vidas: " + v;
        if (keysText != null) OnKeysChanged += (v) => keysText.text = "Llaves: " + v;
        if (infoText != null)
        {
            OnHealthChanged += (v) => infoText.text = "Salud: " + v;
            OnCoinsChanged += (v) => infoText.text = "Monedas: " + v;
            OnLivesChanged += (v) => infoText.text = "Vidas: " + v;
            OnKeysChanged += (v) => infoText.text = "Llaves: " + v;
            OnAchievementUnlocked += (a) => infoText.text = "Logro: " + a;
        }

    }

    protected void TriggerOnHealthChanged(int newHealth)
    {
        OnHealthChanged?.Invoke(newHealth);
    }

    protected void TriggerOnCoinsChanged(int newCoins)
    {
        OnCoinsChanged?.Invoke(newCoins);
    }

    protected void TriggerOnKeysChanged(int newKeys)
    {
        OnKeysChanged?.Invoke(newKeys);
    }

    protected void TriggerOnLivesChanged(int newLives)
    {
        OnLivesChanged?.Invoke(newLives);
    }

    protected void TriggerOnAchievementUnlocked(string achievementName)
    {
        OnAchievementUnlocked?.Invoke(achievementName);
    }

}