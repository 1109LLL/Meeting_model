using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[System.Serializable]
class CreateMeetingSessionResult
{
	public bool success;
	public uint meetingId;
}

public class CreateMeetingSession : MonoBehaviour
{
	private MeshRenderer meshRenderer = null;
	public void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	public void startMeeting()
	{
		StartCoroutine(TryCreateMeetingSession());
	}
	IEnumerator TryCreateMeetingSession()
	{
		UnityWebRequest www = UnityWebRequest.Post("http://192.168.0.9:8080/meeting-session/create", "");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) 
		{
			Color blue = Color.blue;
            meshRenderer.material.color = blue;
			Debug.Log(www.error);
			yield break;
		}

		byte[] buffer = www.downloadHandler.data;
		string json = System.Text.Encoding.UTF8.GetString(buffer);
		var result = JsonUtility.FromJson<CreateMeetingSessionResult>(json);
		if (result.success == false) {
			Color green = Color.green;
            meshRenderer.material.color = green;
			Debug.Log("create meeting session failed!");
			yield break;
		} else {
			Color yellow = Color.yellow;
            meshRenderer.material.color = yellow;
			Debug.Log("creating meeting session succeeded");
			Debug.Log("meeting-session-id: " + result.meetingId);
			yield break;
		}
	}
}

