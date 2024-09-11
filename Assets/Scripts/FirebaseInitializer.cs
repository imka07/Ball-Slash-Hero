using System;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.Messaging;
using Firebase.RemoteConfig;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    private const string ConfigDataKey = "saved_config_data";

    public event Action<string> FirebaseInitialized;
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                var app = FirebaseApp.DefaultInstance;
                var config = FirebaseRemoteConfig.DefaultInstance;

                FirebaseMessaging.TokenReceived += (_, args) => Debug.Log($"Token Received {args}");
                FirebaseMessaging.MessageReceived += (_, args) => Debug.Log($"Message Received {args}");

                var data = string.Empty;
                FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);

                if (PlayerPrefs.HasKey(ConfigDataKey))
                {
                    data = PlayerPrefs.GetString(ConfigDataKey);
                    FirebaseInitialized?.Invoke(data); // Вызываем событие
                }
                else
                {
                    config.FetchAndActivateAsync().ContinueWithOnMainThread(fetchTask =>
                    {
                        if (!fetchTask.IsCompleted || config.Info.LastFetchStatus != LastFetchStatus.Success)
                            return;

                        var configValue = config.GetValue("AccessControlKey").StringValue;

                        PlayerPrefs.SetString(ConfigDataKey, configValue);
                        data = configValue;
                        FirebaseInitialized?.Invoke(data); // Вызываем событие
                    });
                }
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        });
    }
}
