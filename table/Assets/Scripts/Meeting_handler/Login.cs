using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
class login_success_token
{
    public string auth_token;
    public string user_id;
}
[System.Serializable]
class Login_request_payload
{
    public string email;
    public string hashed_password;
}

public class Login : MonoBehaviour
{
    public GameObject notification_board_failure;
    public GameObject notification_board_success;
    public TextMeshProUGUI login_token;
    public TextMeshProUGUI login_id;
    public TextMeshProUGUI email_;
    public TextMeshProUGUI password_;

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

    // void Awake()
    // {
    //     DontDestroyOnLoad(login_id);
    //     DontDestroyOnLoad(login_token);
    // }

    public void Login_verification()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }
        StartCoroutine(TryLogin());
        if (login_token.text != "" && login_id.text != "")
        {
            loadRoomScene();
        }
        timeLeft = DEBOUNCE_TIME_S;
    }

    IEnumerator TryLogin()
    {
        Login_request_payload payload = new Login_request_payload();
        payload.email = "example@example.com";
        // payload.email = email_.text;
        payload.hashed_password = "sO31+7iOCyh+r76az9YIwqaxoOzty+fUTNHNWJA0w+I=";
        // payload.hashed_password = password_.text;
        string json_payload = JsonUtility.ToJson(payload);

        // server address
        string url = "http://192.168.0.9:8080/login";

        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(json_payload);
        webRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(encodedPayload);
        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError) 
		{
			Debug.Log("ERROR: "+ webRequest.error);
            notification_board_failure.SetActive(true);
			yield break;
		}
        
        byte[] buffer = webRequest.downloadHandler.data;
        string json = System.Text.Encoding.UTF8.GetString(buffer);
        var result = JsonUtility.FromJson<login_success_token>(json);
        if(result.auth_token != "" && result.user_id != "")
        {
            Debug.Log("SUCCESSFUL!");
            Debug.Log("auth_token = "+result.auth_token);
            Debug.Log("user_id = "+ result.user_id);

            login_token.text = result.auth_token;
            login_id.text = result.user_id;
            Debug.Log("login_token text :: "+login_token.text);
            notification_board_success.SetActive(true);
        }
        yield break;

    }
    public void loadRoomScene()
    {
        SceneManager.LoadScene(sceneName:"room");
    }

}
