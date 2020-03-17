using MyGame;
using Photon.Pun;
using UnityEngine;

public class FpsAndTpsCameraController : MonoBehaviour
{
    private PhotonView _photonView;
    
    [SerializeField] private MyMainCameraManager myMainCameraManager;
    [SerializeField] private CameraPivot cameraPivot;
    [SerializeField] private CameraMain cameraMain;
    
    [SerializeField] private NetworkPlayerInputController networkPlayerInput;

    [SerializeField] private float currentXAngle;

    [SerializeField] private float clamXAngleMin = -45f;
    [SerializeField] private float clamXAngleMax = 75f;

    // FPS - first person shooter
    // TPS - third person shooter
    [SerializeField] private bool isFps = false;
    [SerializeField] private bool isLeftShoulder = true;
    
    [SerializeField] private Vector3 fpsCamPos = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 fpsPivotPos = new Vector3(0f, 0.7f, 0f);

    [SerializeField] private Vector3 tpsCamPos = new Vector3(0f, 1.5f, -6f);
    [SerializeField] private Vector3 tpsPivotPos = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 leftShoulderOffset = new Vector3(-0.8f, 0f, 0f);
    [SerializeField] private Vector3 rightShoulderOffset = new Vector3(0.8f, 0f, 0f);

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        networkPlayerInput = GetComponent<NetworkPlayerInputController>();

        myMainCameraManager = MyMainCameraManager.Instance;
        cameraPivot = myMainCameraManager.cameraPivot;
        cameraMain = myMainCameraManager.cameraMain;
    }

    private void OnEnable()
    {
        networkPlayerInput.SwitchToFpsOrTps += SwitchToFpsOrTps;
        networkPlayerInput.SwitchShoulder += SwitchShoulder;
    }
    
    private void OnDisable()
    {
        networkPlayerInput.SwitchToFpsOrTps -= SwitchToFpsOrTps;
        networkPlayerInput.SwitchShoulder -= SwitchShoulder;
    }

    private void Start()
    {
        if (!_photonView.IsMine) return;
        
        cameraMain.transform.localPosition = isLeftShoulder ? (tpsCamPos + leftShoulderOffset) : (tpsCamPos + rightShoulderOffset);
        cameraPivot.transform.localPosition = tpsPivotPos;
    }

    private void Update()
    {
        if (!_photonView.IsMine) return;
    
        HandlePosition();
        HandleRotation();
    
        if (!isFps)
        {
            HandleCameraCollisions();
        }
    }

    private void SwitchToFpsOrTps()
    {
        isFps = !isFps;

        if (isFps)
        {
            cameraMain.transform.localPosition = fpsCamPos;
            cameraPivot.transform.localPosition = fpsPivotPos;
        }
        else
        {
            cameraMain.transform.localPosition =
                (isLeftShoulder ? (tpsCamPos + leftShoulderOffset) : (tpsCamPos + rightShoulderOffset)).normalized * maxDistance;
            cameraPivot.transform.localPosition = tpsPivotPos;
        }
    }

    private void SwitchShoulder()
    {
        isLeftShoulder = !isLeftShoulder;
    }

    private void HandlePosition()
    {
        myMainCameraManager.transform.position = transform.position;

        if (isFps)
        {
            cameraMain.transform.localPosition = fpsCamPos;
            cameraPivot.transform.localPosition = fpsPivotPos;
        }
        else
        {
            cameraPivot.transform.localPosition = tpsPivotPos;
        }
    }

    private void HandleRotation()
    {
        float mouseY = networkPlayerInput.DeltaMouseY;
        
        currentXAngle -= mouseY;
        currentXAngle = Mathf.Clamp(currentXAngle, clamXAngleMin, clamXAngleMax);

        Quaternion cameraHolderTargetRotation = transform.rotation;
        myMainCameraManager.transform.rotation = cameraHolderTargetRotation;
        
        Quaternion cameraPivotTargetRotation = Quaternion.Euler(currentXAngle, 0, 0);
        cameraPivot.transform.localRotation = cameraPivotTargetRotation;
    }
    
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 4f;
    [SerializeField] private float smooth = 10f;
    [SerializeField] private float distance;
    [SerializeField] private float multiplier = 0.9f;
    
    [SerializeField] private Vector3 dollyDir;

    private void HandleCameraCollisions()
    {
        dollyDir = (isLeftShoulder ? (tpsCamPos + leftShoulderOffset) : (tpsCamPos + rightShoulderOffset)).normalized;
        distance = cameraMain.transform.localPosition.magnitude;
        
        Vector3 desiredCameraPos = cameraMain.transform.parent.TransformPoint(dollyDir * maxDistance);

        distance = Physics.Linecast(cameraMain.transform.parent.position, desiredCameraPos, out RaycastHit hit,
            LayerMask.GetMask("Default"))
            ? Mathf.Clamp(hit.distance * multiplier, minDistance, maxDistance)
            : maxDistance;

        cameraMain.transform.localPosition = Vector3.Lerp(cameraMain.transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
