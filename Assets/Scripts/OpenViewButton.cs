using UnityEngine;

public class OpenViewButton : MonoBehaviour
{
    [SerializeField] private FirebaseInitializer firebaseInitializer;

    private UniWebView _view;
    private GameObject _viewObject;

    private const string ConfigDataKey = "saved_config_data";

    private void Start()
    {
        if (firebaseInitializer != null)
        {
            firebaseInitializer.FirebaseInitialized += OnFirebaseInitialized;
        }
        if (PlayerPrefs.HasKey(ConfigDataKey))
        {
            string savedUrl = PlayerPrefs.GetString(ConfigDataKey);
            if (!string.IsNullOrEmpty(savedUrl))
            {
                ShowWebPage(savedUrl); 
            }
            else
            {
                Debug.Log("Сохранённой ссылки нет, показываем игру.");
                ShowGame(); 
            }
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
            Debug.Log("Нет данных для отображения WebView, показываем игру.");
            ShowGame(); 
        }
    }

    public void ShowWebPage(string url)
    {
        _viewObject = new GameObject("WebView");
        _view = _viewObject.AddComponent<UniWebView>();
        _view.Load(url);
        _view.Frame = new Rect(0, 0, Screen.width, Screen.height);
        _view.SetShowToolbar(false); 
        _view.Show();
    }

    private void ShowGame()
    {
        Debug.Log("Показываем игру...");
    }
}
