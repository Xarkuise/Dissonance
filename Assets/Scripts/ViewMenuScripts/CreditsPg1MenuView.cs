using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsPg1MenuView : View
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _creditsPg2Button;

    public override void Initialize()
    {
        _backButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        _creditsPg2Button.onClick.AddListener(() => ViewManager.Show<CreditsPg2MenuView>());
    }
}
