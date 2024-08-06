using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TelegramBot : MonoBehaviour
{
    private string botToken = "";
    private string chatId = "";

    [SerializeField]
    private TMP_InputField inputField;

    public void SendMsg(string msg)
    {
        StartCoroutine(SendMessageToTelegram(msg));
    }

    IEnumerator SendMessageToTelegram(string message)
    {
        string json = "{\"chat_id\":\"" + chatId + "\",\"text\":\"" + message + "\"}";

        string url = "https://api.telegram.org/bot" + botToken + "/sendMessage";

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Message sent successfully!");
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}