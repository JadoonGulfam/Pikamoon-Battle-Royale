using UnityEngine;
using Pikamoon.Controller;
using Unity.VisualScripting;
using System.Collections.Generic;
public class HealthPointAndHitPointAssignerEditor : MonoBehaviour
{
    [SerializeField] Transform ReferenceCharacter;
    [SerializeField] Transform NewCharacter;

    [SerializeField] HealthPoint[] healthpoints;
    [SerializeField] HitPoint[] hitPoints;
    [SerializeField] WeaponHitBoxExtension[] weaaponHitExtensions;
    Transform FindDeepChildByPartialName(Transform parent, string partialName)
    {
        foreach (Transform child in parent)
        {
            if (child.name.Contains(partialName))
                return child;

            Transform result = FindDeepChildByPartialName(child, partialName);
            if (result != null)
                return result;
        }
        return null;
    }
    [ContextMenu("Perform GetComponentsFromReference")]
    public void GetComponentsFromReference()
    {
        healthpoints = ReferenceCharacter.GetComponentsInChildren<HealthPoint>();
        hitPoints = ReferenceCharacter.GetComponentsInChildren<HitPoint>();
        weaaponHitExtensions = ReferenceCharacter.GetComponentsInChildren<WeaponHitBoxExtension>();
    }


    [ContextMenu("Perform Action")]
    public void AssignHealthPointAndHitPoints()
    {
        if (healthpoints.Length == 0)
        {
            healthpoints = ReferenceCharacter.GetComponentsInChildren<HealthPoint>();
        }
        for (int i = 0; i < healthpoints.Length; i++)
        {
            SingleHealthPointAction(healthpoints[i]);
        }



        if (weaaponHitExtensions.Length == 0)
        {
            weaaponHitExtensions = ReferenceCharacter.GetComponentsInChildren<WeaponHitBoxExtension>();
        }

        for (int i = 0; i < weaaponHitExtensions.Length; i++)
        {
            SingleWeaponHitBoxEntension(weaaponHitExtensions[i], i);
        }


        if (hitPoints.Length == 0)
        {
            hitPoints = ReferenceCharacter.GetComponentsInChildren<HitPoint>();
        }
        for (int i = 0; i < hitPoints.Length; i++)
        {
            SingleHitPointAction(hitPoints[i]);
        }

    }


    void SingleHealthPointAction(HealthPoint HP)
    {
        GameObject obj = new GameObject();

        Transform parent = FindDeepChildByPartialName(NewCharacter.transform, HP.transform.parent.name);
        obj.transform.parent = parent;
        obj.name = HP.name;
        obj.transform.localPosition = HP.transform.localPosition;
        obj.transform.localRotation = HP.transform.localRotation;
        obj.transform.localScale = HP.transform.localScale;

        obj.layer = HP.gameObject.layer;
        obj.tag = HP.gameObject.tag;


        obj.AddComponent<HealthPoint>();
        HealthPoint newHP = obj.GetComponent<HealthPoint>();
        newHP.player = NewCharacter.GetComponent<HealthController>();
        newHP.type = HP.type;

        if (HP.GetComponent<Collider>() is BoxCollider)
        {
            BoxCollider collider = HP.GetComponent<BoxCollider>();
            BoxCollider bc = obj.AddComponent<BoxCollider>();

            bc.isTrigger = true;
            bc.center = collider.center;
            bc.size = collider.size;

            bc.layerOverridePriority = collider.layerOverridePriority;
            bc.excludeLayers = collider.excludeLayers;
            bc.includeLayers = collider.includeLayers;
        }
        else if (HP.GetComponent<Collider>() is CapsuleCollider)
        {
            CapsuleCollider collider = HP.GetComponent<CapsuleCollider>();


            CapsuleCollider bc = obj.AddComponent<CapsuleCollider>();

            bc.isTrigger = true;
            bc.center = collider.center;
            bc.radius = collider.radius;
            bc.height = collider.height;
            bc.direction = collider.direction;

            bc.layerOverridePriority = collider.layerOverridePriority;
            bc.excludeLayers = collider.excludeLayers;
            bc.includeLayers = collider.includeLayers;
        }
    }
    void SingleHitPointAction(HitPoint HitP)
    {

        Transform hitPoint = FindDeepChildByPartialName(NewCharacter.transform, HitP.transform.name);

        hitPoint.gameObject.layer = HitP.gameObject.layer;
        hitPoint.gameObject.tag = HitP.gameObject.tag;


        if (hitPoint.GetComponent<Collider>() is BoxCollider)
        {
            BoxCollider collider = hitPoint.GetComponent<BoxCollider>();
            BoxCollider referenceCollider = HitP.GetComponent<BoxCollider>();

            collider.isTrigger = true;
            collider.center = referenceCollider.center;
            collider.size = referenceCollider.size;

            collider.layerOverridePriority = referenceCollider.layerOverridePriority;
            collider.excludeLayers = referenceCollider.excludeLayers;
            collider.includeLayers = referenceCollider.includeLayers;
        }
        else if (hitPoint.GetComponent<Collider>() is SphereCollider)
        {
            SphereCollider collider = hitPoint.GetComponent<SphereCollider>();
            SphereCollider referenceCollider = HitP.GetComponent<SphereCollider>();

            collider.isTrigger = true;
            collider.center = referenceCollider.center;
            collider.radius = referenceCollider.radius;

            collider.layerOverridePriority = referenceCollider.layerOverridePriority;
            collider.excludeLayers = referenceCollider.excludeLayers;
            collider.includeLayers = referenceCollider.includeLayers;
        }


        Rigidbody rb;

        if (hitPoint.GetComponent<Rigidbody>() == null)
        {
            rb = hitPoint.AddComponent<Rigidbody>();
        }
        else
        {
            rb = hitPoint.GetComponent<Rigidbody>();
        }

        Rigidbody referenceRB = HitP.GetComponent<Rigidbody>();


        if (rb == null)
        {
            Debug.Log("Rb Null");
        }
        if (referenceRB == null)
        {
            Debug.Log("referenceRB Null");
        }


        rb.isKinematic = referenceRB.isKinematic;
        rb.useGravity = referenceRB.isKinematic;

        rb.collisionDetectionMode = referenceRB.collisionDetectionMode;
        rb.excludeLayers = referenceRB.excludeLayers;
        rb.includeLayers = referenceRB.includeLayers;

    }


    public GameObject obj;
    void SingleWeaponHitBoxEntension(WeaponHitBoxExtension ReferenceWHitExtP, int index)
    {
        Transform WeaponHitExt = FindDeepChildByPartialName(NewCharacter.transform, ReferenceWHitExtP.transform.name);

        if (WeaponHitExt == null)
        {
            Debug.Log("Already Not has with same name");
            obj = new GameObject();


            Transform ReferenceParent = FindDeepChildByPartialName(ReferenceCharacter.transform, ReferenceWHitExtP.transform.parent.name);
            Transform Newparent = FindDeepChildByPartialName(NewCharacter.transform, ReferenceParent.name);

            obj.transform.parent = Newparent;
            obj.name = ReferenceWHitExtP.name;
            obj.transform.localPosition = ReferenceWHitExtP.transform.localPosition;
            obj.transform.localRotation = ReferenceWHitExtP.transform.localRotation;
            obj.transform.localScale = ReferenceWHitExtP.transform.localScale;

            obj.gameObject.layer = ReferenceWHitExtP.gameObject.layer;
            obj.gameObject.tag = ReferenceWHitExtP.gameObject.tag;



            WeaponHitBoxExtension ext = obj.AddComponent<WeaponHitBoxExtension>();


            if (ReferenceWHitExtP.GetComponent<Collider>() is BoxCollider)
            {
                BoxCollider collider = obj.AddComponent<BoxCollider>();
                BoxCollider referenceCollider = ReferenceWHitExtP.GetComponent<BoxCollider>();

                collider.isTrigger = true;
                collider.center = referenceCollider.center;
                collider.size = referenceCollider.size;

                collider.layerOverridePriority = referenceCollider.layerOverridePriority;
                collider.excludeLayers = referenceCollider.excludeLayers;
                collider.includeLayers = referenceCollider.includeLayers;

                ext._collider = collider;

            }
            else if (ReferenceWHitExtP.GetComponent<Collider>() is SphereCollider)
            {
                SphereCollider collider = obj.AddComponent<SphereCollider>();
                SphereCollider referenceCollider = ReferenceWHitExtP.GetComponent<SphereCollider>();

                collider.isTrigger = true;
                collider.center = referenceCollider.center;
                collider.radius = referenceCollider.radius;

                collider.layerOverridePriority = referenceCollider.layerOverridePriority;
                collider.excludeLayers = referenceCollider.excludeLayers;
                collider.includeLayers = referenceCollider.includeLayers;

                ext._collider = collider;
            }


            if (ReferenceWHitExtP.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                Rigidbody referenceRB = ReferenceWHitExtP.GetComponent<Rigidbody>();

                rb.isKinematic = referenceRB.isKinematic;
                rb.useGravity = referenceRB.isKinematic;

                rb.collisionDetectionMode = referenceRB.collisionDetectionMode;
                rb.excludeLayers = referenceRB.excludeLayers;
                rb.includeLayers = referenceRB.includeLayers;
            }

            HitBehaviour newCharacterHitBehaviour = NewCharacter.GetComponent<HitBehaviour>();

            newCharacterHitBehaviour.extensionForWeaponHoldingPoint = ReferenceCharacter.GetComponent<HitBehaviour>().extensionForWeaponHoldingPoint;

            if (index == 0)
            {
                newCharacterHitBehaviour.extensionForWeaponHoldingPoint[1].weaponHitBoxExtensions[0] = ext;
            }
            else if (index == 1)
            {
                newCharacterHitBehaviour.extensionForWeaponHoldingPoint[1].weaponHitBoxExtensions[1] = ext;
            }
            else if (index == 2)
            {
                newCharacterHitBehaviour.extensionForWeaponHoldingPoint[0].weaponHitBoxExtensions[0] = ext;
            }
            else if (index == 3)
            {
                newCharacterHitBehaviour.extensionForWeaponHoldingPoint[0].weaponHitBoxExtensions[1] = ext;
            }

        }
        else
        {
            Debug.Log("Already has with same name", WeaponHitExt);
        }
    }

}
