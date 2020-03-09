using UnityEngine;

public class HandleWeaponEquipment : MonoBehaviour
{
    public void Equip(Transform targetWeaponTransform)
    {
        gameObject.SetActive(true);
        transform.SetParent(targetWeaponTransform);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public void UnEquip()
    {
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
