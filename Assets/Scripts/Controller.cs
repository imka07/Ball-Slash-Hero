using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    UniWebView webView;

    // Start is called before the first frame update
    void Start()
    {
        // Create a game object to hold UniWebView and add component.
        var webViewGameObject = new GameObject("UniWebView");
        webView = webViewGameObject.AddComponent<UniWebView>();

        // More to add...

        webView.Frame = new Rect(0, 0, Screen.width, Screen.height); // 1
        webView.Load("https://docs.uniwebview.com/game.html");       // 2
        webView.Show();

        webView.OnPageFinished += (view, statusCode, url) =>
        {
            // Page load finished
        };


        webView.OnShouldClose += (view) => {
            webView = null;
            return true;
        };


        webView.OnPageFinished += (view, statusCode, url) => {
            webView.EvaluateJavaScript("startGame();", (payload) => {
                if (payload.resultCode.Equals("0"))
                {
                    Debug.Log("Game Started!");
                }
                else
                {
                    Debug.Log("Something goes wrong: " + payload.data);
                }
            });
        };

    }
}