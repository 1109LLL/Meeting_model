using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.Text;
using System;


public class LogOut : MonoBehaviour
{
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
    public void logout()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }

        StartCoroutine(tryLogout());
        if (feedback.text.Equals("Successfully logged out!"));
        {
            SceneManager.LoadScene(sceneName:"LoginPage");
        }
        timeLeft = DEBOUNCE_TIME_S;
    }

    IEnumerator tryLogout()
    {
        feedback.text = "In coroutine";
        authentication payload = new authentication();
        payload.auth_token = login_token.text;
        payload.uuid = login_id.text;
        string json_payload = JsonUtility.ToJson(payload);

        Debug.Log("STRING == "+json_payload);
        byte[] bytesToEncode = Encoding.UTF8.GetBytes (json_payload);
        string encodedText = Convert.ToBase64String (bytesToEncode);
        Debug.Log("Encoded AUTH_PAYLOAD = "+ encodedText);
        // server address
        string url = "http://192.168.0.9:8080/logout";

        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + encodedText);
        
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError) 
		{
			Debug.Log("ERROR: "+ webRequest.error);
            feedback.text = "Log out failed :: "+webRequest.responseCode + " :: " +webRequest.error;

			yield break;
		}
        
        
        if (webRequest.responseCode == 204)
        {
            feedback.text = "Successfully logged out!";
            Debug.Log("Successfully logged out");
        }
        yield break;
    }
}
