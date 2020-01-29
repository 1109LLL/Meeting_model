using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leave_meeting_button : MonoBehaviour
{
    public Button leaveButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = leaveButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log("leaveButton::onClick");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
