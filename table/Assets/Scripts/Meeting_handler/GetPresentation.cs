using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using System;

public class GetPresentation : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI meetingID;
    public TextMeshProUGUI login_token;
    public TextMeshProUGUI login_id;
    public TextMeshProUGUI feedback;

    public RawImage presentation_image;

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
    public void getPresentation()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }

        StartCoroutine(tryGetPresentation());

        timeLeft = DEBOUNCE_TIME_S;
    }

    IEnumerator tryGetPresentation()
    {
        // create authen_payload
        feedback.text = "In leave coroutine";
        authentication payload = new authentication();
        payload.auth_token = login_token.text;
        payload.uuid = login_id.text;
        string json_payload = JsonUtility.ToJson(payload);

        Debug.Log("STRING == "+json_payload);
        byte[] bytesToEncode = Encoding.UTF8.GetBytes (json_payload);
        string encodedText = Convert.ToBase64String (bytesToEncode);
        Debug.Log("Encoded AUTH_PAYLOAD = "+ encodedText);

        // server address
        string url = "http://192.168.0.9:8080/meetings/"+meetingID.text+"/presentation";

        // sending requests
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + encodedText);
        yield return webRequest.SendWebRequest();


        if (webRequest.isNetworkError || webRequest.isHttpError) 
		{
			Debug.Log("ERROR: "+ webRequest.error);
            // notification_board_failure.SetActive(true);
            feedback.text = "Get presentatino failed :: "+webRequest.responseCode + " :: " +webRequest.error;

			yield break;
		}
        
        if (webRequest.responseCode == 200)
        {
            // notification_board_success.SetActive(true);
            feedback.text = "Get presentation success";
            Debug.Log("Successfully obtained presentation");
            Texture myTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            byte[] results = webRequest.downloadHandler.data;

            //Converting byte results of the presentation image into 2D texture
            Texture2D presentationTexture = new Texture2D(2,2);
            bool isLoaded = presentationTexture.LoadImage(results);

            //Apply 2D texture onto raw image -> presentation_image
            if (isLoaded)
            {
                presentation_image = presentation_image.GetComponent<RawImage>();
                presentation_image.texture = presentationTexture;

                presentation_image.enabled = true;
            }
        }
        yield break;
    }
}
