using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public AvatarGender activePlayerGender;
    public AvatarData maleAvatarData;
    public AvatarData femaleAvatarData;
    private void Awake()
    {
        
    }
    private void Start()
    {
            //ActivateAvatarByGender(AvatarGender.Male);
    }

    public void ActivateAvatarByGender(string gender)
    {
        switch (gender)
        {
            case "Male":
                maleAvatarData.avatar_parent.gameObject.SetActive(true);
                femaleAvatarData.avatar_parent.gameObject.SetActive(false);
                UpdateAvatarRefrences(maleAvatarData);
                break;
            case "Female":
                maleAvatarData.avatar_parent.gameObject.SetActive(false);
                femaleAvatarData.avatar_parent.gameObject.SetActive(true);
                UpdateAvatarRefrences(femaleAvatarData);
                break;
        }

    }

    private void UpdateAvatarRefrences(AvatarData _avatarData)
    {
        if (_avatarData.avatar_parent.GetComponent<EyesBlinking>() != null)
        {
            _avatarData.avatar_parent.GetComponent<EyesBlinking>().StoreBlendShapeValues();
            if (activePlayerGender != _avatarData.avatar_Gender)
            {
                StartCoroutine(_avatarData.avatar_parent.GetComponent<EyesBlinking>().BlinkingStartRoutine());
            }
        }

        activePlayerGender = _avatarData.avatar_Gender;

            //GameManager.Instance.mainCharacter = _avatarData.avatar_parent;
                     
    }
 

    public AvatarData GetActiveAvatarData()
    {
        if (activePlayerGender == AvatarGender.Male)
        {
            return maleAvatarData;
        }
        else
        {
            return femaleAvatarData;
        }
    }


    [Serializable]
    public class AvatarData
    {
        public AvatarGender avatar_Gender;
        public GameObject avatar_parent;
        public SkinnedMeshRenderer avatar_body;
        public Animator avatar_animator;
        public Texture DShirt_Texture, DPent_Texture, DShoe_Texture, DEye_texture, DFace_Texture, DSkin_Texture;
    }
}
public enum AvatarGender
{
    Male, Female
}