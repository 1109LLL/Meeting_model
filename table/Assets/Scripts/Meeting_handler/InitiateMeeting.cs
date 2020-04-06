using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.Text;
using System;

[System.Serializable]
class initiate_success
{
    public string meeting_id;
    public string presenter;
    public string[] listeners;
    public string started_at;
}
public class InitiateMeeting : MonoBehaviour
{
    public TextMeshProUGUI meetingID;
    public TextMeshProUGUI login_token;
    public TextMeshProUGUI login_id;
    public TextMeshProUGUI feedback;
    
    private const float DEBOUNCE_TIME_S = 0.23f;
    private float timeLeft = 0.0f;
    void start()
    {
        timeLeft = DEBOUNCE_TIME_S;
    }

    void Update()
    {
        if (timeLeft >= 0.0f) {
            timeLeft -= Time.deltaTime;
        }
    }
    public void initiate_meeting_session()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }

        StartCoroutine(tryInitiateMeeting());

        timeLeft = DEBOUNCE_TIME_S;
    }

    IEnumerator tryInitiateMeeting()
    {
        Debug.Log("Trying to initiate meeting...");
        authentication payload = new authentication();
        payload.auth_token = login_token.text;
        payload.uuid = login_id.text;

        string json_payload = JsonUtility.ToJson(payload);

        Debug.Log("STRING == "+json_payload);
        byte[] bytesToEncode = Encoding.UTF8.GetBytes (json_payload);
        string encodedText = Convert.ToBase64String (bytesToEncode);
        Debug.Log("Encoded AUTH_PAYLOAD = "+ encodedText);
        // server address
        string url = "http://192.168.0.9:8080/meetings";

        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + encodedText);
        
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError) 
		{
			Debug.Log("ERROR: "+ webRequest.error);
            Debug.Log("Initiate meeting failed");
            feedback.text = "Initiate meeting failed :: "+webRequest.responseCode + " :: " +webRequest.error;
			yield break;
		}
        
        byte[] buffer = webRequest.downloadHandler.data;
        string json = System.Text.Encoding.UTF8.GetString(buffer);
        var result = JsonUtility.FromJson<initiate_success>(json);

        if (webRequest.responseCode == 201)
        {
            feedback.text = "Initiate meeting successful";
            meetingID.text = result.meeting_id;
            Debug.Log("Initiate meeting SUCCESS!");
        }
        yield break;
    }
}
