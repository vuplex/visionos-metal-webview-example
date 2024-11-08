using UnityEngine;
using Vuplex.WebView;

/// <summary>
/// Provides a simple example of using 3D WebView's scripting APIs.
/// </summary>
/// <remarks>
/// Links: <br/>
/// - CanvasWebViewPrefab docs: https://developer.vuplex.com/webview/CanvasWebViewPrefab <br/>
/// - How clicking works: https://support.vuplex.com/articles/clicking <br/>
/// - Other examples: https://developer.vuplex.com/webview/overview#examples <br/>
/// </remarks>
public class VisionOSMetalWebViewExample : MonoBehaviour {

    public bool EnablePassthrough = true;

    CanvasWebViewPrefab canvasWebViewPrefab;

    void Awake() {

        // Use a desktop User-Agent to request the desktop versions of websites.
        // https://developer.vuplex.com/webview/Web#SetUserAgent
        Web.SetUserAgent(false);
    }

    async void Start() {

        _enablePassthroughIfNeeded();

        // Get a reference to the CanvasWebViewPrefab.
        // https://support.vuplex.com/articles/how-to-reference-a-webview
        canvasWebViewPrefab = GameObject.Find("CanvasWebViewPrefab").GetComponent<CanvasWebViewPrefab>();

        // Wait for the prefab to initialize because its WebView property is null until then.
        // https://developer.vuplex.com/webview/WebViewPrefab#WaitUntilInitialized
        await canvasWebViewPrefab.WaitUntilInitialized();

        // After the prefab has initialized, you can use the IWebView APIs via its WebView property.
        // https://developer.vuplex.com/webview/IWebView
        canvasWebViewPrefab.WebView.UrlChanged += (sender, eventArgs) => {
            Debug.Log("[VisionOSWebViewExample] URL changed: " + eventArgs.Url);
        };
    }

    void _enablePassthroughIfNeeded() {

        if (!EnablePassthrough) {
            return;
        }
        var camera = Camera.main;
        camera.clearFlags = CameraClearFlags.Color;
        // A clear color with alpha = 0 is required to show passthrough, as well as setting the Metal Immersion Style to Automatic or Mixed.
        // You must reset the background color every time, because setting the clear flags to Skybox will override it.
        camera.backgroundColor = Color.clear;        
    }
}