using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterCustomization
{
    public class CharacterHandler : MonoBehaviour
    {
        [Serializable]
        public class AvatarData
        {
            public GenderType avatar_Gender;
            public GameObject avatar_parent;
            public SkinnedMeshRenderer avatar_body;
            public Animator avatar_animator;
            public Texture  DEye_texture, DSkin_Texture;
        }
    }
    [Serializable]
    public class AvatarDefaultClothes
    {
        public Texture2D DefaultEyes,DefaultEyebrow, DefaultSkin;
        public GameObject DefaultBody, DefaultHair;
        public Color DefaultSkinColor, DefaultLipsColor;
    }
    [Serializable]
    public class CharacterData
    {
        public string gender;
        public string faceShape;
        public string eyeShape;
        public string lipsShape;
        public string noseShape;
      // public float armShape;
       // public float legShape;
        public string torsoShape;
        public string hairPreset;
        public string shirtPreset;
        public string trouserPreset;
        public string shoesPreset;
        public string armPreset;
        public string legPreset;
        public string eyeColor;
        public string eyeBrowShape;
        public string skinColor;
        public Color hairColor;
      //  public Color lipsColor;
        public string characterPreset;
        public CharacterData Clone()
        {
            return (CharacterData)this.MemberwiseClone();
        }
    }
}
public enum BodyType { Face, Hair, Lips, Eyes, Nose, SkinColor, EyeColor, Outfit , Preset, Torso, Eyebrow, Cap, Body  }
public enum GenderType { male, female }

public enum BodyPartsType { Chest, Hips, Hair, Feet, Arms, Legs, Eyes, Eyebrow }
//public enum SliderType
//{
//    Arms, Legs, Torso
//}