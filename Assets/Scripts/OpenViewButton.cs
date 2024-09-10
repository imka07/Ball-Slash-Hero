using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenViewButton : MonoBehaviour
{
    [SerializeField] private string target;

    private UniWebView _view;
    private GameObject _viewObject;

    private Rect _rect;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowWebPage);
    }

    private void OnDestroy() => _button.onClick.RemoveListener(ShowWebPage);

    private void ShowWebPage()
    {
        AudioListener.volume = 0f;
        _viewObject = new GameObject();
        _view = _viewObject.AddComponent<UniWebView>();
        _view.Load(target);
        _view.Frame = new Rect(0, 0, Screen.width, Screen.height);
        _view.SetShowToolbar(true);
        _view.Show();
        _view.OnShouldClose += _ => Close();
    }

    private bool Close()
    {
        AudioListener.volume = 1f;
        Destroy(_viewObject);
        _view = null;
        return true;
    }
}