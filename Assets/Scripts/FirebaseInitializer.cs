using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    private const string ConfigDataKey = "saved_config_data";

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                var config = FirebaseRemoteConfig.DefaultInstance;

                if (PlayerPrefs.HasKey(ConfigDataKey))
                {
                    string savedLink = PlayerPrefs.GetString(ConfigDataKey);

                    if (!string.IsNullOrEmpty(savedLink))
                    {
                        OpenWebView(savedLink);
                    }
                    else
                    {
                        ShowGame();
                    }
                }
                else
                {
                    FetchRemoteConfig(config);
                }
            }
            else
            {
                Debug.LogError($"Не удалось разрешить все зависимости Firebase: {task.Result}");
            }
        });
    }

    private void FetchRemoteConfig(FirebaseRemoteConfig config)
    {
        config.FetchAndActivateAsync().ContinueWithOnMainThread(fetchTask =>
        {
            if (fetchTask.IsCompleted && config.Info.LastFetchStatus == LastFetchStatus.Success)
            {
                string configValue = config.GetValue("AccessControlKey").StringValue;

                PlayerPrefs.SetString(ConfigDataKey, configValue);

                if (!string.IsNullOrEmpty(configValue))
                {
                    OpenWebView(configValue);
                }
                else
                {
                    ShowGame();
                }
            }
            else
            {
                Debug.LogError("Ошибка при получении данных из Remote Config.");
                ShowGame(); 
            }
        });
    }

    private void OpenWebView(string url)
    {
        GameObject webViewObject = new GameObject("WebView");
        UniWebView webView = webViewObject.AddComponent<UniWebView>();
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        webView.Load(url);
        webView.Show();
    }

    private void ShowGame()
    {
        Debug.Log("Ссылка пустая, запускаем игру.");
    }
}

