using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OpenViewButton : MonoBehaviour
{
    [SerializeField] private FirebaseInitializer firebaseInitializer;
    private UniWebView _view;
    private GameObject _viewObject;

    private void Start()
    {
        if (firebaseInitializer != null)
        {
            firebaseInitializer.FirebaseInitialized += OnFirebaseInitialized;
        }
    }

    private void OnDestroy()
    {
        if (firebaseInitializer != null)
        {
            firebaseInitializer.FirebaseInitialized -= OnFirebaseInitialized;
        }
    }

    private void OnFirebaseInitialized(string configData)
    {
        if (!string.IsNullOrEmpty(configData))
        {
            ShowWebConfig(configData);
        }
        else
        {
            Debug.Log("Нет данных для отображения WebView");
        }
    }

    public void ShowWebView(string url)
    {
        _viewObject = new GameObject("WebView");
        _view = _viewObject.AddComponent<UniWebView>();
        _view.Load(url);
        _view.Frame = new Rect(0, 0, Screen.width, Screen.height);
        _view.SetShowToolbar(true);
        _view.Show();
    }

    private void ShowWebConfig(string url)
    {
        AudioListener.volume = 0f;
        _viewObject = new GameObject();
        _view = _viewObject.AddComponent<UniWebView>();
        _view.Load(url);
        _view.Frame = new Rect(0, 0, Screen.width, Screen.height);
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
