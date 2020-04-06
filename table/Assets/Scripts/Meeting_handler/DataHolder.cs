using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI login_token;
    public TextMeshProUGUI login_id;

    static public string token_0 = "";
    static public string id_0 = "";

    void Start()
    {
        
        login_token.text = token_0;
        login_id.text = id_0;

    }

    // Update is called once per frame
    void Update()
    {
        token_0 = login_token.text;
        id_0 = login_id.text;
    }

}
