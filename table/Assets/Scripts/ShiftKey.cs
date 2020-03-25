using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public GameObject lower_case_letters;
    public GameObject upper_case_letters;
    public GameObject shift_key_lower;
    public GameObject shift_key_upper;
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

    
}
