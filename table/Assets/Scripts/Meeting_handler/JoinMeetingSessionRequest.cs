using UnityEngine;

[System.Serializable]
public class JoinMeetingSessionRequest
{
	public JoinMeetingSessionRequest(string clientId)
	{
		this.clientId = clientId;
	}

	public string clientId { get; set; }
}

