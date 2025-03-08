using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;

    public override void Initialize()
    {
        _startButton.onClick.AddListener(() => ViewManager.Show<StartMenuView>());
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());
        _creditsButton.onClick.AddListener(() => ViewManager.Show<CreditsPg1MenuView>());

    }
}
