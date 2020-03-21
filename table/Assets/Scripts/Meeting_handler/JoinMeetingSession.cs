using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Security.Cryptography;

public class JoinMeetingSession : MonoBehaviour
{
	// private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
	// private static string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
	// private static uint CLIENT_ID_LENGTH = 16;

	// void Start()
	// {
	// 	StartCoroutine(TryJoinMeeting());
	// }

	// IEnumerator TryJoinMeeting(uint meetingId)
	// {
	// 	string url = String.Format("http://localhost:8080/meeting-session/join/{0}", meetingId);
	// 	string clientId = GenerateClientId();
	// 	var requestPayload = new JoinMeetingSessionRequest(clientId);
	// 	var requestPayloadJson = JsonUtility.ToJson(requestPayload);
	// 	UnityWebRequest www = UnityWebRequest.Post(url, requestPayloadJson);
	// 	yield return www.SendWebRequest();

	// 	if (www.isNetworkError || www.isHttpError)
	// 	{
	// 		Debug.Log(www.error);
	// 	}
	// 	else
	// 	{
	// 		Debug.Log("Successfully joined meeting session " + meetingId);
	// 	}
	// }

	// private string GenerateClientId()
	// {
	// 	var buffer = new char[CLIENT_ID_LENGTH];
	// 	for (int i = 0; i < CLIENT_ID_LENGTH; i++)
	// 	{
	// 		buffer[i] = CHARS[rng.Next(CHARS.Length)];
	// 	}
	// 	return new String(buffer);
	// }
}
