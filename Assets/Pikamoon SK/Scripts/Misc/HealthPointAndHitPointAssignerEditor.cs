using UnityEngine;
using Pikamoon.Controller;
using Unity.VisualScripting;
public class HealthPointAndHitPointAssignerEditor : MonoBehaviour
{
    [SerializeField] Transform NewCharacter;

    [SerializeField] HealthPoint[] healthpoints;
    [SerializeField] HitPoint[] hitPoints;

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


    [ContextMenu("Perform Action")]
    public void AssignHealthPointAndHitPoints()
    {
        for (int i = 0; i < healthpoints.Length; i++)
        {
            SingleHealthPointAction(healthpoints[i]);
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
        obj.transform.localScale    = HP.transform.localScale;

        obj.layer = HP.gameObject.layer;
        obj.tag = HP.gameObject.tag;


        obj.AddComponent<HealthPoint>();
        HealthPoint newHP = obj.GetComponent<HealthPoint>();
        newHP.player = NewCharacter.GetComponent<HealthController>();
        newHP.type = HP.type;

        if(HP.GetComponent<Collider>() is BoxCollider)
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


        Rigidbody rb          = hitPoint.AddComponent<Rigidbody>();
        Rigidbody referenceRB = HitP.GetComponent<Rigidbody>();

        rb.isKinematic = referenceRB.isKinematic;
        rb.useGravity = referenceRB.isKinematic;

        rb.collisionDetectionMode = referenceRB.collisionDetectionMode;
        rb.excludeLayers = referenceRB.excludeLayers;
        rb.includeLayers = referenceRB.includeLayers;

    }
}
