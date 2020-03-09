﻿using System.Collections.Generic;
using MyGame;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(NetworkPlayerInputController))]
public class PlayerNetworkWeaponController : MonoBehaviourPun
{
    [SerializeField] private List<GameObject> weaponsToEquip = new List<GameObject>();
    [SerializeField] private List<Transform> weaponTransformsPoints = new List<Transform>();
    [SerializeField] private List<Weapon> equippedWeapons = new List<Weapon>();

    private NetworkPlayerInputController _networkPlayerInputController;

    private void Awake()
    {
        _networkPlayerInputController = GetComponent<NetworkPlayerInputController>();
        
        FindWeaponsPositions();
        InitializeWeapons();
    }

    private void OnEnable()
    {
        _networkPlayerInputController.Fire += FireFromAllWeapons;
    }
    
    private void OnDisable()
    {
        _networkPlayerInputController.Fire -= FireFromAllWeapons;
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < weaponTransformsPoints.Count; i++)
        {
            GameObject weapon = Instantiate(weaponsToEquip[i], Vector3.zero, Quaternion.identity);
            Weapon weaponScript = weapon.GetComponent<Weapon>();
            HandleWeaponEquipment handleWeaponEquipment = weapon.GetComponent<HandleWeaponEquipment>();

            equippedWeapons.Add(weaponScript);
            handleWeaponEquipment.Equip(weaponTransformsPoints[i]);
        }
    }

    private void FindWeaponsPositions()
    {
        WeaponPosition[] weaponPositions = GetComponentsInChildren<WeaponPosition>(true);
        foreach (WeaponPosition weaponPosition in weaponPositions)
        {
            weaponTransformsPoints.Add(weaponPosition.transform);
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