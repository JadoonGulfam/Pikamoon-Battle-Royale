using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    [Serializable]
    public class AvatarData
    {
        public GenderType avatar_Gender;
        public GameObject avatar_parent;
        public SkinnedMeshRenderer avatar_body;
        public Animator avatar_animator;
        public Texture DEye_texture, DSkin_Texture;
    }
}
[Serializable]
public class AvatarDefaultClothes
{
   // public Texture2D DefaultEyes, DefaultEyebrow, DefaultSkin, DefaultFace;
    public GameObject DefaultBody, DefaultHair;
   // public Color DefaultLipsColor;
}
[Serializable]
public class CharacterData
{
    public string gender;
    public string faceShape;
    public string eyeShape;
    public string lipsShape;
    public string earsShape;
    public string noseShape;
    public string torsoShape;
    public string hairPreset;
    public string clothPreset;
    public string eyeColor;
    public string eyeBrowShape;
    public string skinColor;
    public Color hairColor;
    public int index;
    public string name;
    public string characterPreset;
    public CharacterData Clone()
    {
        return (CharacterData)this.MemberwiseClone();
    }
}

public enum BodyType { Face, Hair, Lips, Eyes, Nose, SkinColor, EyeColor, Outfit, Preset, Torso, Eyebrow, Cap, Body, Ears }
public enum GenderType { male, female }

public enum BodyPartsType { Body, Hair, Eyes, Eyebrow, Skin }
