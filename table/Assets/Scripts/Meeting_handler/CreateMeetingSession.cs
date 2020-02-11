using UnityEngine;
// using UnityEngine.__Networking__;
// using UnityEngine.JSONSerializeModule;
using System.Collections;
using UnityEngine.Networking;

// [System.Serializable]
// class CreateMeetingSessionResult
// {
// 	public bool success;
// 	public uint meetingId;
// }

public class CreateMeetingSession : MonoBehaviour
{
	public Animator animator; 

	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
	}
	public void startMeeting()
	{
		StartCoroutine(TryCreateMeetingSession());
	}
	IEnumerator TryCreateMeetingSession()
	{
		UnityWebRequest www = UnityWebRequest.Post("http://10.97.160.121:8080/meeting-session/create", "");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			stopWaving();
			Debug.Log(www.error);
			yield break;
		}

		byte[] buffer = www.downloadHandler.data;
		string json = System.Text.Encoding.UTF8.GetString(buffer);
		var result = JsonUtility.FromJson<CreateMeetingSessionResult>(json);
		if (result.success == false) {
			Debug.Log("create meeting session failed!");
			yield break;
		} else {
			printing();
			startWaving();
			Debug.Log("creating meeting session succeeded");
			Debug.Log("meeting-session-id: " + result.meetingId);
			yield break;
		}
	}

	public void startWaving()
	{
		animator.SetFloat("InputY", -1);
	}

	public void stopWaving()
	{
		animator.SetFloat("InputY", 0);
	}

	public void printing()
	{
		Debug.Log("TESTING");
	}
}

