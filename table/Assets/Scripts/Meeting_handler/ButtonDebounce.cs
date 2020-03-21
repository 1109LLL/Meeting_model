using UnityEngine;
using UnityEngine.UI;

public class ButtonDebounce : MonoBehaviour
{
    public Button joinMeetingButton;

    private const float DEBOUNCE_TIME_S = 1.0f;
    private float timeLeft = 0.0f;

    void Start()
    {
        joinMeetingButton.onClick.AddListener(HandleJoinMeetingSession);
        timeLeft = DEBOUNCE_TIME_S;
    }

    void Update()
    {
        if (timeLeft >= 0.0f) {
            timeLeft -= Time.deltaTime;
        }
    }

    void HandleJoinMeetingSession()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status; not invoking join logic!");
            return;
        }

        // TODO: invoke join meeting logic
        // Reset timer.
        Debug.Log("button finished cooldown; invoking join logic!");
        timeLeft = DEBOUNCE_TIME_S;
    }
}

