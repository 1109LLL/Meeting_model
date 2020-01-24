using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_switch : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject presenter_cam;
    public GameObject camera_1;
    public GameObject camera_2;
    public GameObject camera_3;
    public GameObject camera_4;
    public GameObject camera_5;
    public GameObject camera_6;

    void Start()
    {
        //Camera Position Set
        System.Console.WriteLine(PlayerPrefs.GetInt("CameraPosition"));
        
    }

    // Update is called once per frame
    void Update()
    {
        //Change Camera Keyboard
        switchCamera();
    }

    //Change Camera Keyboard
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            cameraPositionChange(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraPositionChange(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraPositionChange(2);
        }
    }

    //Camera change Logic
    void cameraPositionChange(int target_camera)
    {
        int camera_number = PlayerPrefs.GetInt("CameraPosition");

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", target_camera);

        //Set camera position 0
        if (target_camera == 0)
        {
            presenter_cam.SetActive(true);
            deactivate_prev_camera(camera_number);
        }
        //Set camera position 1
        else if (target_camera == 1)
        {
            camera_1.SetActive(true);
            deactivate_prev_camera(camera_number);
        }
        else if (target_camera == 2)
        {
            camera_2.SetActive(true);
            deactivate_prev_camera(camera_number);
        }

    }

    void deactivate_prev_camera(int camera_number)
    {
        if (camera_number == 0)
        {
            presenter_cam.SetActive(false);
        }
        else if (camera_number == 1)
        {
            camera_1.SetActive(false);
        }
        else if (camera_number == 2)
        {
            camera_2.SetActive(false);
        }
    }
}
