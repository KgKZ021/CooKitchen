using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInvverted
    }

    [SerializeField] private Mode mode;
    private void LateUpdate() //run after regular update
    {
        //oriententation logic should be after whatevvver that object do in update
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform); // make one transform look anohter transform.look at camera
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position; //givves us dir pointing from camera
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInvverted:
                transform.forward= -Camera.main.transform.forward;
                break;

        }
        
        

    }
}
