using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenSafeBrowsingButton : MonoBehaviour
{
    [SerializeField] private string targetUrl;

    private UniWebViewSafeBrowsing _safeBrowsing;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenSafeBrowsing);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OpenSafeBrowsing);
    }

    private void OpenSafeBrowsing()
    {
        // Проверка, поддерживает ли устройство Safe Browsing Mode
        if (!UniWebViewSafeBrowsing.IsSafeBrowsingSupported)
        {
            Debug.LogError("Safe Browsing Mode is not supported on this device.");
            return;
        }

        // Проверка валидности URL
        if (string.IsNullOrEmpty(targetUrl))
        {
            Debug.LogError("Target URL is null or empty.");
            return;
        }

        // Если URL не начинается с http:// или https://, добавляем http://
        if (!targetUrl.StartsWith("http://") && !targetUrl.StartsWith("https://"))
        {
            targetUrl = "http://" + targetUrl;
        }

        // Создаем экземпляр UniWebViewSafeBrowsing с URL
        _safeBrowsing = UniWebViewSafeBrowsing.Create(targetUrl);

        // Настраиваем цвет панели инструментов (например, зеленый фон и белые элементы)
        _safeBrowsing.SetToolbarColor(new Color(0.263f, 0.627f, 0.278f)); // Зеленый фон
        _safeBrowsing.SetToolbarItemColor(Color.white); // Белые элементы (только для iOS)

        // Подписка на событие завершения просмотра
        _safeBrowsing.OnSafeBrowsingFinished += OnSafeBrowsingFinished;

        // Показываем окно Safe Browsing
        _safeBrowsing.Show();
    }

    // Обработчик события завершения просмотра
    private void OnSafeBrowsingFinished(UniWebViewSafeBrowsing browsing)
    {
        Debug.Log("Safe Browsing session finished.");
        // Очистка ресурсов
        _safeBrowsing = null;
    }
}
