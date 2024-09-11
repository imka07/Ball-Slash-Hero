using UnityEngine;

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
            ShowWebPage(configData);
        }
        else
        {
            Debug.Log("Нет данных для отображения WebView");
        }
    }

    public void ShowWebPage(string url)
    {
        _viewObject = new GameObject("WebView");
        _view = _viewObject.AddComponent<UniWebView>();
        _view.Load(url);
        _view.Frame = new Rect(0, 0, Screen.width, Screen.height);
        _view.SetShowToolbar(true);
        _view.Show();
    }
}
