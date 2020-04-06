using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using System;

[System.Serializable]
class participants_info
{
    public string presenter;
    public string[] listeners;
    public string started_at;
}

[System.Serializable]
class user_info
{
    public string first_name;
    public string last_name;
}

public class getNameTag : MonoBehaviour
{
    public TextMeshProUGUI meetingID;
    public TextMeshProUGUI feedback;
    public TextMeshProUGUI nameTag0;
    public TextMeshProUGUI nameTag1;
    public TextMeshProUGUI nameTag2;
    public TextMeshProUGUI nameTag3;
    public TextMeshProUGUI nameTag4;

    public string[] listenerNames;

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
    public void GetNameTags()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }

        StartCoroutine(tryGetParticipantsInfo());
        int i = 0;
        foreach (string IDs in listenerNames)
        {
            StartCoroutine(tryGetNames(IDs, i));
            i++;
        }
        timeLeft = DEBOUNCE_TIME_S;
    }

    IEnumerator tryGetParticipantsInfo()
    {

        // server address
        string url = "http://192.168.0.9:8080/meetings/"+meetingID.text;

        // sending requests
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();


        if (webRequest.isNetworkError || webRequest.isHttpError) 
		{
			Debug.Log("ERROR: "+ webRequest.error);
            feedback.text = "Get userName failed :: "+webRequest.responseCode + " :: " +webRequest.error;

			yield break;
		}

        byte[] buffer = webRequest.downloadHandler.data;
        string json = System.Text.Encoding.UTF8.GetString(buffer);
        var result = JsonUtility.FromJson<participants_info>(json);
        
        if (webRequest.responseCode == 200)
        {
            feedback.text = "Get participant's info success";
            Debug.Log("Successfully obtained participants' ids");

            listenerNames = result.listeners;
            
        }
        yield break;
    }

    IEnumerator tryGetNames(string id, int position)
    {

        // server address
        string url = "http://192.168.0.9:8080/accounts/"+id;

        // sending requests
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();


        if (webRequest.isNetworkError || webRequest.isHttpError) 
		{
			Debug.Log("ERROR: "+ webRequest.error);
            feedback.text = "Get userName failed :: "+webRequest.responseCode + " :: " +webRequest.error;

			yield break;
		}

        byte[] buffer = webRequest.downloadHandler.data;
        string json = System.Text.Encoding.UTF8.GetString(buffer);
        var result = JsonUtility.FromJson<user_info>(json);
        
        if (webRequest.responseCode == 200)
        {
            feedback.text = "Get userName success";
            Debug.Log("Successfully obtained user name");

            string nameTagLabel = "nameTag"+position;
            TextMeshProUGUI nameTag = GameObject.FindWithTag(nameTagLabel).GetComponent<TextMeshProUGUI>();
            nameTag.text = result.first_name + " " + result.last_name;
        }
        yield break;
    }
}
