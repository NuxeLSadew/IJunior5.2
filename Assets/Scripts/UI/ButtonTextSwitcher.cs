using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonTextSwitcher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _textIsInteractable;
    [SerializeField] private string _textIsNotInteractable;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    public void UpdateText()
    {
        _text.text = _button.interactable ? _textIsInteractable : _textIsNotInteractable;
    }
}
