using UnityEngine;
using NativeWebSocket;
using UnityEngine.Events;
using NaughtyAttributes;

public class WSConnection : MonoBehaviour
{
    public UnityEvent<string> OnMessageReceived;

    WebSocket websocket = null;
    [SerializeField, ReadOnly] bool _isWsOpen = false;
    
    [SerializeField] bool _showLogs = false;

    // Open the websocket communication on the configured port
    async void Start()
    {
        AsyncOpenWS();
    }
    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (_isWsOpen)
            websocket.DispatchMessageQueue();
#endif
    }

    /// <summary>
    /// Close the websocket communication
    /// </summary>
    public void CloseWS()
    {
        if (websocket != null)
            AsyncCloseWS();
    }

    /// <summary>
    /// Open the websocket communication on the configured port
    /// </summary>
    async void AsyncOpenWS()
    {
        websocket = new WebSocket($"ws://localhost:8080");

        websocket.OnOpen += () =>
        {
            if(_showLogs) Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            if (_showLogs) Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            if (_showLogs) Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            if (_showLogs)
            {
                Debug.Log($"OnMessage: {message}");
            }

            OnMessageReceived?.Invoke(message);
        };

        // Keep sending messages at every 0.3s
        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        _isWsOpen = true;
        // waiting for messages
        await websocket.Connect();
    }

    /// <summary>
    /// Close the websocket communication
    /// </summary>
    async void AsyncCloseWS()
    {
        await websocket.Close();
        _isWsOpen = false;
        websocket = null;
    }


    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("plain text message");
        }
    }

    async void SendWSMessage(string msg)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending plain text
            await websocket.SendText(msg);
        }
    }

    async void SendWSMessage(byte[] msg)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(msg);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
