using UnityEngine;
using UnityEngine.UI;

public class CreditsPg2MenuView : View
{
    [SerializeField] private Button _creditsPg1Button;

    public override void Initialize()
    {
        _creditsPg1Button.onClick.AddListener(() => ViewManager.Show<CreditsPg1MenuView>());
    }
}
