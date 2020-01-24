using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRRaycaster : MonoBehaviour {

	[System.Serializable]
	public class Callback : UnityEvent<Ray, RaycastHit> {}

	public Transform leftHandAnchor = null;
	public Transform rightHandAnchor = null;
	public Transform centerEyeAnchor = null;
    private Transform _selection;
	public LineRenderer lineRenderer = null;
	public float maxRayDistance = 500.0f;
	public LayerMask excludeLayers;
	public VRRaycaster.Callback raycastHitCallback;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material defaultMaterial;
	public static GameObject hitObject;

	void Awake() {
		if (leftHandAnchor == null) {
			Debug.LogWarning ("Assign LeftHandAnchor in the inspector!");
			GameObject left = GameObject.Find ("LeftHandAnchor");
			if (left != null) {
				leftHandAnchor = left.transform;
			}
		}
		if (rightHandAnchor == null) {
			Debug.LogWarning ("Assign RightHandAnchor in the inspector!");
			GameObject right = GameObject.Find ("RightHandAnchor");
			if (right != null) {
				rightHandAnchor = right.transform;
			}
		}
		if (centerEyeAnchor == null) {
			Debug.LogWarning ("Assign CenterEyeAnchor in the inspector!");
			GameObject center = GameObject.Find ("CenterEyeAnchor");
			if (center != null) {
				centerEyeAnchor = center.transform;
			}
		}
		if (lineRenderer == null) {
			Debug.LogWarning ("Assign a line renderer in the inspector!");
			lineRenderer = gameObject.AddComponent<LineRenderer> ();
			lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			lineRenderer.receiveShadows = false;
			lineRenderer.widthMultiplier = 0.02f;
		}
	}

	Transform Pointer {
		get {
			OVRInput.Controller controller = OVRInput.GetConnectedControllers();
			if ((controller & OVRInput.Controller.LTrackedRemote) != OVRInput.Controller.None) {
				return leftHandAnchor;
			} else if ((controller & OVRInput.Controller.RTrackedRemote) != OVRInput.Controller.None) {
				return rightHandAnchor;
			}
			// If no controllers are connected, we use ray from the view camera. 
			// This looks super ackward! Should probably fall back to a simple reticle!
			return centerEyeAnchor;
		}
	}

	void Update() {
		Transform pointer = Pointer;
		if (pointer == null) {
			return;
		}
        
        // if (_selection != null)
        // {
        //     var selectionRenderer = _selection.GetComponent<Renderer>();
        //     selectionRenderer.material = defaultMaterial;
        //     _selection = null;
        // }

		Ray laserPointer = new Ray (pointer.position, pointer.forward);

		if (lineRenderer != null) {
			lineRenderer.SetPosition (0, laserPointer.origin);
			lineRenderer.SetPosition (1, laserPointer.origin + laserPointer.direction * maxRayDistance);
		}
		
		RaycastHit hit;
		// if (Physics.Raycast (laserPointer, out hit, maxRayDistance, ~excludeLayers))
		if (Physics.Raycast (laserPointer, out hit, maxRayDistance))
        {

			if (hit.collider)
			{
				lineRenderer.SetPosition (1, hit.point);
			}

			// if (lineRenderer != null) 
            // {
			// 	lineRenderer.SetPosition (1, hit.point);
			// }

			// if (raycastHitCallback != null) 
            // {
			// 	raycastHitCallback.Invoke (laserPointer, hit);
			// }

			// var selection = hit.transform;
			// hitObject = hit.collider.gameObject;
			// if (hitObject.CompareTag(selectableTag))
			// {
			// 	var selectionRenderer = hitObject.GetComponent<Renderer>();
            //     if (selectionRenderer != null)
            //     {
            //         selectionRenderer.material = highlightMaterial;
            //     }

            //     _selection = selection;
			// }


            // var selection = hit.transform;
            // if (selection.CompareTag(selectableTag))
            // {
            //     var selectionRenderer = selection.GetComponent<Renderer>();
            //     if (selectionRenderer != null)
            //     {
            //         selectionRenderer.material = highlightMaterial;
            //     }

            //     _selection = selection;
            // }
		}
	}
}