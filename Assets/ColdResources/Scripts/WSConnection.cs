using UnityEngine;
using NativeWebSocket;
using UnityEngine.Events;
using NaughtyAttributes;

public class WSConnection : MonoBehaviour
{
    public UnityEvent<string> OnMessageReceived;

    WebSocket websocket = null;
    [SerializeField, ReadOnly] bool _isWsOpen = false;
    [SerializeField, ReadOnly] bool _tryReopening = false;
    bool _openInstructionCalled = false;
    
    [SerializeField] bool _showLogs = false;
    [SerializeField, ShowIf("_showLogs")] bool _showMessages = false;

    // Open the websocket communication on the configured port
    async void Start()
    {
        AsyncOpenWS();
    }
    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (_tryReopening && !_openInstructionCalled)
            AsyncOpenWS();
            
        if (_isWsOpen && !_openInstructionCalled)
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
            if (_showLogs) Debug.Log("Connection open!");
            _openInstructionCalled = false;
            _tryReopening = false;
            _isWsOpen = true;
        };

        websocket.OnError += (e) =>
        {
            if (_showLogs) Debug.Log("Error! " + e);

        };

        websocket.OnClose += (e) =>
        {
            if (_showLogs) Debug.Log("Connection closed!");

            _openInstructionCalled = false;
            _tryReopening = true;
            _isWsOpen = false;
        };

        websocket.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            if (_showLogs && _showMessages)
            {
                Debug.Log($"OnMessage: {message}");
            }

            OnMessageReceived?.Invoke(message);
        };

        // Keep sending messages at every 0.3s
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
        _openInstructionCalled = true;
        // waiting for messages
        await websocket.Connect();
    }

    /// <summary>
    /// Close the websocket communication
    /// </summary>
    async void AsyncCloseWS()
    {
        _isWsOpen = false;
        await websocket.Close();
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
        _isWsOpen = false;
        await websocket.Close();
    }
}
