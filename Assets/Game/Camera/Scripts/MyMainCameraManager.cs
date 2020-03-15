using UnityEngine;

public class MyMainCameraManager : MonoBehaviour
{
    public static MyMainCameraManager Instance;
    public Camera myCamera;
    
    public CameraPivot cameraPivot;
    public CameraMain cameraMain;
    private void Awake()
    {
        Instance = this;
        
        myCamera = Camera.main;

        cameraPivot = GetComponentInChildren<CameraPivot>();
        cameraMain = GetComponentInChildren<CameraMain>();
    }
}
