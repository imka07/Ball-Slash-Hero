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
            // Подписываемся на событие инициализации Firebase
            firebaseInitializer.FirebaseInitialized += OnFirebaseInitialized;
        }

        // Проверяем, есть ли уже сохранённая ссылка
        if (PlayerPrefs.HasKey(ConfigDataKey))
        {
            string savedUrl = PlayerPrefs.GetString(ConfigDataKey);
            if (!string.IsNullOrEmpty(savedUrl))
            {
                ShowWebPage(savedUrl); // Открываем вебвью, если ссылка есть
            }
            else
            {
                Debug.Log("Сохранённой ссылки нет, показываем игру.");
                ShowGame(); // Если ссылки нет, показываем игру
            }
        }
    }

    private void OnDestroy()
    {
        if (firebaseInitializer != null)
        {
            // Отписываемся от события
            firebaseInitializer.FirebaseInitialized -= OnFirebaseInitialized;
        }
    }

    // Метод вызывается, когда данные с Firebase загружены
    private void OnFirebaseInitialized(string configData)
    {
        if (!string.IsNullOrEmpty(configData))
        {
            ShowWebPage(configData); // Показываем WebView, если есть данные
        }
        else
        {
            Debug.Log("Нет данных для отображения WebView, показываем игру.");
            ShowGame(); // Если данные пустые, показываем игру
        }
    }

    // Метод для отображения WebView
    public void ShowWebPage(string url)
    {
        _viewObject = new GameObject("WebView");
        _view = _viewObject.AddComponent<UniWebView>();
        _view.Load(url);
        _view.Frame = new Rect(0, 0, Screen.width, Screen.height);
        _view.SetShowToolbar(false); // Скрываем тулбар, так как кнопок закрытия быть не должно
        _view.Show();

        // WebView не закрывается, как указано в вашем алгоритме
    }

    // Метод для отображения игры
    private void ShowGame()
    {
        Debug.Log("Показываем игру...");
        // Здесь можно добавить логику для показа игрового интерфейса
    }
}
