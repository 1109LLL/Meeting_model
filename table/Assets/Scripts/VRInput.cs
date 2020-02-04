using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// The BaseInput is an Interface to the Input system used by the BaseInputModule.

// The BaseInputModule is a component of the EventSystem 
// that is responsible for raising events and sending them to GameObjects 
// for handling. The BaseInputModule is a class that all Input Modules 
// in the EventSystem inherit from.

// This script overrides the BaseInputModule with OVRInput signals.

public class VRInput : BaseInput
{
    public Camera eventCamera = null;
    public OVRInput.Button clickbutton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller controller = OVRInput.Controller.All;
    protected override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;
    }

    // Mouse button clicks are replaced by controller clicks
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
        // mousePosition is replaced by the centre of the event camera
        get
        {
            return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2);
        }
    }
}
