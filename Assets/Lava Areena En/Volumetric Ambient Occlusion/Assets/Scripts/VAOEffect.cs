// Copyright (c) 2016-2018 Jakub Boksansky - All Rights Reserved
// Volumetric Ambient Occlusion Unity Plugin 2.0
/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;
#endif
using System;
using System.Collections.Generic;
using System.Reflection;


namespace Wilberforce.VAO
{

    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [HelpURL("https://projectwilberforce.github.io/vaomanual/")]
    [AddComponentMenu("Image Effects/Rendering/Volumetric Ambient Occlusion(CGSOSO.COM:For trial use only, otherwise,pls purchase from original store!)")]
    public class VAOEffect : VAOEffectCommandBuffer
    {
        [ImageEffectOpaque]
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            this.PerformOnRenderImage(source, destination);
        }

    }


#if UNITY_EDITOR

    [CustomEditor(typeof(VAOEffect))]
    public class VAOEffectEditorImageEffect : VAOEffectEditor { }

#endif
}
