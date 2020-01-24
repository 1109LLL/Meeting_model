﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class VR_Input : BaseInput
{
    public Camera eventCamera = null;
    public OVRInput.Button clickbutton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller controller = OVRInput.Controller.All;
    protected override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;
    }

    public override bool GetMouseButton(int button)
    {
        return OVRInput.Get(clickbutton, controller);
    }

    public override bool GetMouseButtonDown(int button)
    {
        return OVRInput.GetDown(clickbutton, controller);
    }

    public override bool GetMouseButtonUp(int button)
    {
        return OVRInput.GetUp(clickbutton, controller);
    }

    public override Vector2 mousePosition
    {
        get
        {
            return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2);
        }
    }
}
