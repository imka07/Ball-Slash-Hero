using Firebase.RemoteConfig;
using Firebase.Extensions;
using System;
using UnityEngine;

public class FirebaseConfigManager : MonoBehaviour
{
    private void Start()
    {
        // Инициализация Firebase
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FetchRemoteConfig();
        });
    }

    private void FetchRemoteConfig()
    {
        // Загрузка данных Remote Config
        FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread(fetchTask =>
        {
            if (fetchTask.IsCompleted)
            {
                FirebaseRemoteConfig.DefaultInstance.ActivateAsync().ContinueWithOnMainThread(activateTask =>
                {
                    // Данные успешно загружены
                });
            }
            else
            {
                Debug.LogError("Ошибка загрузки данных Firebase Remote Config");
            }
        });
    }
}
