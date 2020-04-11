using System.Collections.Generic;
using Extensions;
using MyGame;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(NetworkPlayerInputController))]
public class PlayerNetworkWeaponController : MonoBehaviourPun
{
    [SerializeField] private TargetLook targetLook;
    [SerializeField] private CameraMain cameraMain;
    [SerializeField] private CameraPivot cameraPivot;
    [SerializeField] private float maxTargetLookPos = 20f;
    
    [SerializeField] private List<GameObject> weaponsToEquip = new List<GameObject>();
    [SerializeField] private List<Transform> weaponTransformPoints = new List<Transform>();
    [SerializeField] private List<Weapon> equippedWeapons = new List<Weapon>();
    [SerializeField] private List<Transform> weaponPivotTransforms = new List<Transform>();

    private NetworkPlayerInputController _networkPlayerInputController;

    private void Awake()
    {
        _networkPlayerInputController = GetComponent<NetworkPlayerInputController>();

        // if (!photonView.IsMine) return;
        
        FindWeaponsPositions();
    }

    private void Start()
    {
        targetLook = MyMainCameraManager.Instance.targetLook;
        cameraMain = MyMainCameraManager.Instance.cameraMain;
        cameraPivot = MyMainCameraManager.Instance.cameraPivot;
        
        InitializeWeapons();
        
        if (!photonView.IsMine) return;
        
        SetTargetLookPos(maxTargetLookPos);
    }

    private void OnEnable()
    {
        _networkPlayerInputController.Fire += FireFromAllWeapons;
    }
    
    private void OnDisable()
    {
        _networkPlayerInputController.Fire -= FireFromAllWeapons;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        
        HandleWeaponRotation();
    }

    private void HandleWeaponRotation()
    {
        const float distance = 20f;

        Ray ray = new Ray(cameraMain.transform.position, cameraMain.transform.forward * distance);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            foreach (Transform transformPoint in weaponTransformPoints)
            {
                float distFromPlayerToPoint = (hit.point - cameraMain.transform.position).magnitude;
                if (distFromPlayerToPoint >= 3)
                {
                    Vector3 direction = transformPoint.DirectionTo(hit.point);
                
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transformPoint.rotation = lookRotation;
                }
            }
        }
        else
        {
            foreach (Transform transformPoint in weaponTransformPoints)
            {
                Vector3 direction = transformPoint.DirectionTo(cameraMain.transform.forward * distance);
                
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transformPoint.rotation = lookRotation;
            }
        }
    }

    private void SetTargetLookPos(float distance)
    {
        targetLook.transform.localPosition = targetLook.transform.forward * distance;
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < weaponsToEquip.Count; i++)
        {
            GameObject weapon = Instantiate(weaponsToEquip[i], Vector3.zero, Quaternion.identity);
            Weapon weaponScript = weapon.GetComponent<Weapon>();
            HandleWeaponEquipment handleWeaponEquipment = weapon.GetComponent<HandleWeaponEquipment>();

            equippedWeapons.Add(weaponScript);
            handleWeaponEquipment.Equip(weaponPivotTransforms[i]);
            
            if (photonView.IsMine)
            {
                weaponTransformPoints[i].SetParent(cameraPivot.transform);
                weaponTransformPoints[i].localPosition =
                    weaponTransformPoints[i].localPosition.With(i % 2 == 0 ? 1f : -1f, -0.6f, -0.3f);
            }
        }
    }

    private void FindWeaponsPositions()
    {
        WeaponPosition[] weaponPositions = GetComponentsInChildren<WeaponPosition>(true);
        foreach (WeaponPosition weaponPosition in weaponPositions)
        {
            weaponTransformPoints.Add(weaponPosition.transform);
            weaponPivotTransforms.Add(weaponPosition.GetComponentInChildren<WeaponPivot>().transform);
        }
    }

    private void FireFromAllWeapons()
    {
        photonView.RPC("NetworkFireFromAllWeapons", RpcTarget.All);
    }

    [PunRPC]
    private void NetworkFireFromAllWeapons()
    {
        foreach (Weapon equippedWeapon in equippedWeapons)
        {
            equippedWeapon.Fire(photonView.Owner);
        }
    }
}