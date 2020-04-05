using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKButton : MonoBehaviour
{
    public GameObject notification_board;

    public void hide_board()
    {
        notification_board.SetActive(false);
    }
}
