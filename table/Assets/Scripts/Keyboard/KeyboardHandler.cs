using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class KeyboardHandler : MonoBehaviour
{
    public GameObject lower_case_letters;
    public GameObject upper_case_letters;
    public GameObject shift_key_lower;
    public GameObject shift_key_upper;
    public GameObject key;
    public GameObject username_keyboard;
    public GameObject password_keyboard;
    public TextMeshProUGUI Inputfield;

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
    public void shift_cases()
    {
        if(lower_case_letters != null && upper_case_letters != null )
        {
            bool low_isActive = lower_case_letters.activeSelf;
            bool up_isActive = upper_case_letters.activeSelf;

            lower_case_letters.SetActive(!low_isActive);
            shift_key_lower.SetActive(!low_isActive);
            upper_case_letters.SetActive(!up_isActive);
            shift_key_upper.SetActive(!up_isActive);
        }
    }
    
    public void input()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }

        string existing_text = Inputfield.text;
        string new_text = EventSystem.current.currentSelectedGameObject.name;
        Inputfield.text = string.Format("{0}{1}", existing_text, new_text);
        timeLeft = DEBOUNCE_TIME_S;
    }

    public void backspace()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }

        string existing_text = Inputfield.text;
        if (existing_text != "")
        {
            Inputfield.text = existing_text.Substring(0, existing_text.Length - 1);
        }
        timeLeft = DEBOUNCE_TIME_S;
    }

    public void space()
    {
        if (timeLeft > 0.0f)
        {
            Debug.Log("button is in debounce status");
            return;
        }

        string existing_text = Inputfield.text;
        Inputfield.text = existing_text + " ";
        timeLeft = DEBOUNCE_TIME_S;
    }

    public void username_selected()
    {
        if (username_keyboard.activeSelf)
        {
            username_keyboard.SetActive(false);
        }
        else
        {
            username_keyboard.SetActive(true);
            password_keyboard.SetActive(false);
        }
    }

    public void password_selected()
    {
        if (password_keyboard.activeSelf)
        {
            password_keyboard.SetActive(false);
        }
        else
        {
            password_keyboard.SetActive(true);
            username_keyboard.SetActive(false);
        }
    }
}
