/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Editor.Managers
{
    using Opsive.Shared.Editor.UIElements.Managers;
    using Opsive.UltimateCharacterController.Character;
    using Opsive.UltimateCharacterController.Editor.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Animations;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.UIElements;

    /// <summary>
    /// Draws the Animation Replacer settings within the window.
    /// </summary>
    [Serializable]
    [OrderedEditorItem("Animation Replacer", 6)]
    public class AnimationReplacer : Manager
    {
        private VisualElement m_Container;

        private AnimatorController m_AnimatorController;
        private AnimationReplacements m_ReplacementsTemplate;
        private bool m_ReplaceEvents = true;

        private SortedDictionary<AnimationClip, AnimationClip> m_Animations = new SortedDictionary<AnimationClip, AnimationClip>(new AnimationClipComparer());
        private int m_ReplacedCount;

        /// <summary>
        /// Comparerer for Animation Clip.
        /// </summary>
        private class AnimationClipComparer : IComparer<AnimationClip>
        {
            /// <summary>
            /// Compares Animation Clip x to y.
            /// </summary>
            /// <param name="x">The first Animation Clip.</param>
            /// <param name="y">The second Animation Clip.</param>
            /// <returns>x compared to y.</returns>
            public int Compare(AnimationClip x, AnimationClip y)
            {
                return x.name.CompareTo(y.name);
            }
        }

        /// <summary>
        /// Initializes the Animation Replacer.
        /// </summary>
        /// <param name="character">The character that has the Animator.</param>
        /// <param name="replacementsTemplate">The template to replace the animations with.</param>
        public void Initialize(GameObject character, AnimationReplacements replacementsTemplate)
        {
            Animator characterAnimator;
            var modelManager = character.GetComponent<ModelManager>();
            if (modelManager != null) {
                characterAnimator = modelManager.ActiveModel.GetComponent<Animator>();
            } else {
                characterAnimator = character.GetComponentInChildren<AnimatorMonitor>(true).GetComponent<Animator>();
            }
            if (characterAnimator != null) {
                m_AnimatorController = (AnimatorController)characterAnimator.runtimeAnimatorController;
            }
            m_ReplacementsTemplate = replacementsTemplate;

            ShowReplacer();
        }

        /// <summary>
        /// Adds the visual elements to the ManagerContentContainer visual element. 
        /// </summary>
        public override void BuildVisualElements()
        {
            m_Container = new VisualElement();
            m_ManagerContentContainer.Add(m_Container);
            ShowReplacer();
        }

        /// <summary>
        /// Shows the replacer UI.
        /// </summary>
        private void ShowReplacer()
        {
            if (m_Container == null) {
                return;
            }

            m_Container.Clear();

            var horizontalLayout = new VisualElement();
            horizontalLayout.AddToClassList("horizontal-layout");
            horizontalLayout.style.flexShrink = 0;
            horizontalLayout.style.marginTop = 5;
            m_Container.Add(horizontalLayout);

            var startLinkLabel = new Label("New animations can be generated and downloaded with ");
            horizontalLayout.Add(startLinkLabel);
            var linkConfigLabel = new Label(string.Format("<color={0}>Omni Animation</color>", EditorGUIUtility.isProSkin ? "#00aeff" : "#0000ee"));
            linkConfigLabel.RegisterCallback<ClickEvent>(c =>
            {
                Application.OpenURL("https://omnianimation.ai");
            });
            linkConfigLabel.enableRichText = true;
            linkConfigLabel.AddToClassList("hyperlink");
            horizontalLayout.Add(linkConfigLabel);
            var endClickLabel = new Label(".");
            endClickLabel.style.marginLeft = -3;
            horizontalLayout.Add(endClickLabel);

            var animatorControllerField = new ObjectField("Animator Controller");
            animatorControllerField.objectType = typeof(AnimatorController);
            animatorControllerField.allowSceneObjects = false;
            animatorControllerField.value = m_AnimatorController;
            animatorControllerField.RegisterValueChangedCallback(c =>
            {
                m_AnimatorController = (AnimatorController)c.newValue;
                ShowReplacer();
            });
            m_Container.Add(animatorControllerField);

            var replacementTemplateField = new ObjectField("Replacement Template");
            replacementTemplateField.objectType = typeof(AnimationReplacements);
            replacementTemplateField.allowSceneObjects = false;
            replacementTemplateField.value = m_ReplacementsTemplate;
            replacementTemplateField.RegisterValueChangedCallback(c =>
            {
                m_ReplacementsTemplate = (AnimationReplacements)c.newValue;
                ShowReplacer();
            });
            m_Container.Add(replacementTemplateField);

            var replaceEventsToggle = new Toggle("Replace Events");
            replaceEventsToggle.value = m_ReplaceEvents;
            replaceEventsToggle.RegisterValueChangedCallback(c =>
            {
                m_ReplaceEvents = c.newValue;
                ShowReplacer();
            });
            m_Container.Add(replaceEventsToggle);

            if (m_AnimatorController == null || m_AnimatorController.layers.Length == 0) {
                return;
            }

            if (m_ReplacementsTemplate != null) {
                m_ReplacementsTemplate.Initialize();
            }
            PopulateAnimations();
            ShowAnimations(m_Container);

            var replaceButton = new Button();
            replaceButton.text = "Replace";
            replaceButton.clicked += ReplaceAnimations;
            m_Container.Add(replaceButton);
        }

        /// <summary>
        /// Searches through the Animator Controller for any Animation Clips.
        /// </summary>
        private void PopulateAnimations()
        {
            m_Animations.Clear();

            if (m_AnimatorController == null) {
                return;
            }

            Func<AnimationClip, AnimationClip> onAction = (AnimationClip animationClip) =>
            {
                if (m_Animations.ContainsKey(animationClip)) {
                    return null;
                }
                AnimationClip replacementClip = null;
                if (m_ReplacementsTemplate != null) {
                    m_ReplacementsTemplate.ReplacementMap.TryGetValue(animationClip, out replacementClip);
                }
                m_Animations.Add(animationClip, replacementClip);
                return null;
            };
            TraverseTree(m_AnimatorController.layers, onAction);
        }

        /// <summary>
        /// Traverses the Animator Controller tree.
        /// </summary>
        /// <param name="layers">The layers should be traversed.</param>
        /// <param name="onAction">The action that should be taken for each traversed motion.</param>
        private void TraverseTree(AnimatorControllerLayer[] layers, Func<AnimationClip, AnimationClip> onAction)
        {
            for (int i = 0; i < layers.Length; ++i) {
                var stateMachine = layers[i].stateMachine;
                for (int j = 0; j < stateMachine.stateMachines.Length; ++j) {
                    TraverseTree(stateMachine.stateMachines[j], onAction);
                }
                TraverseTree(stateMachine, onAction);
            }
        }

        /// <summary>
        /// Traverses the Animator Controller tree.
        /// </summary>
        /// <param name="childStateMachine">The child state machine should be traversed.</param>
        /// <param name="onAction">The action that should be taken for each traversed motion.</param>
        private object TraverseTree(ChildAnimatorStateMachine childStateMachine, Func<AnimationClip, AnimationClip> onAction)
        {
            var stateMachine = childStateMachine.stateMachine;
            for (int i = 0; i < stateMachine.stateMachines.Length; ++i) {
                TraverseTree(stateMachine.stateMachines[i], onAction);
            }
            TraverseTree(stateMachine, onAction);
            return stateMachine;
        }

        /// <summary>
        /// Traverses the Animator Controller tree.
        /// </summary>
        /// <param name="stateMachine">The state machine should be traversed.</param>
        /// <param name="onAction">The action that should be taken for each traversed motion.</param>
        private void TraverseTree(AnimatorStateMachine stateMachine, Func<AnimationClip, AnimationClip> onAction)
        {
            for (int i = 0; i < stateMachine.states.Length; ++i) {
                var state = stateMachine.states[i].state;
                var motion = TraverseTree(state.motion, onAction);
                if (motion != null) {
                    state.motion = motion;
                }
            }
        }

        /// <summary>
        /// Traverses the Animator Controller tree.
        /// </summary>
        /// <param name="parent">The parent of the motion.</param>
        /// <param name="motion">The motion should be traversed.</param>
        /// <param name="onAction">The action that should be taken for each traversed motion.</param>
        private Motion TraverseTree(Motion motion, Func<AnimationClip, AnimationClip> onAction)
        {
            if (motion is BlendTree) {
                return TraverseTree(motion as BlendTree, onAction);
            }

            var animationClip = motion as AnimationClip;
            if (animationClip == null) {
                return motion;
            }
            return onAction(animationClip);
        }

        /// <summary>
        /// Traverses the Animator Controller tree.
        /// </summary>
        /// <param name="childBlendTree">The blend tree that should be traversed.</param>
        /// <param name="onAction">The action that should be taken for each traversed motion.</param>
        private Motion TraverseTree(BlendTree blendTree, Func<AnimationClip, AnimationClip> onAction)
        {
            var children = blendTree.children;
            for (int i = 0; i < children.Length; ++i) {
                var childMotion = children[i];
                if (childMotion.motion != null) {
                    var motion = TraverseTree(childMotion.motion, onAction);
                    if (motion != null) {
                        childMotion.motion = motion;
                        children[i] = childMotion;
                    }
                }
            }
            blendTree.children = children;
            return blendTree;
        }

        /// <summary>
        /// Shows a list of all of the animations.
        /// </summary>
        /// <param name="container">The parent VisualElement.</param>
        private void ShowAnimations(VisualElement container)
        {
            var scrollView = new ScrollView();
            scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            scrollView.style.paddingTop = scrollView.style.paddingBottom = 5;
            container.Add(scrollView);

            foreach (var animationClipPair in m_Animations) {
                var animationClipField = new ObjectField(animationClipPair.Key.name);
                animationClipField.objectType = typeof(AnimationClip);
                animationClipField.value = animationClipPair.Value;
                animationClipField.RegisterValueChangedCallback(c =>
                {
                    m_Animations[animationClipPair.Key] = (AnimationClip)c.newValue;
                });
                scrollView.Add(animationClipField);
            }
        }

        /// <summary>
        /// Replaces the animations.
        /// </summary>
        public void ReplaceAnimations()
        {
            if (m_AnimatorController == null || m_Animations.Count == 0) {
                return;
            }

            Func<AnimationClip, AnimationClip> onAction = (AnimationClip animationClip) =>
            {
                var value = m_Animations[animationClip];
                if (value == null) {
                    return null;
                }

                // Add the animation events at the same relative time.
                if (m_ReplaceEvents) {
                    var animationEvents = animationClip.events;
                    if (animationEvents.Length > 0) {
                        var newAnimationEvents = new AnimationEvent[animationEvents.Length];
                        for (int i = 0; i < animationEvents.Length; ++i) {
                            var animationEvent = animationEvents[i];
                            if (animationEvent == null) {
                                continue;
                            }

                            var newAnimationEvent = new AnimationEvent();
                            newAnimationEvent.functionName = animationEvent.functionName;
                            newAnimationEvent.floatParameter = animationEvent.floatParameter;
                            newAnimationEvent.intParameter = animationEvent.intParameter;
                            newAnimationEvent.stringParameter = animationEvent.stringParameter;
                            newAnimationEvent.objectReferenceParameter = animationEvent.objectReferenceParameter;
                            newAnimationEvent.messageOptions = animationEvent.messageOptions;
                            newAnimationEvent.time = Mathf.Clamp01(animationEvent.time / animationClip.length);
                            newAnimationEvents[i] = newAnimationEvent;
                        }
                        SetAnimationEvents(value, newAnimationEvents);
                    }
                }

                m_ReplacedCount++;
                return value;
            };
            TraverseTree(m_AnimatorController.layers, onAction);

            Debug.Log(m_ReplacedCount + " animation clips were replaced.");
            Shared.Editor.Utility.EditorUtility.SetDirty(m_AnimatorController);

            if (m_ReplacedCount > 0) {
                ShowReplacer();
                m_ReplacedCount = 0;
            }
        }

        /// <summary>
        /// Sets the animation events on the specified clip.
        /// </summary>
        /// <param name="clip">The clip that should have its animation events set.</param>
        /// <param name="animationEvents">The new animation events.</param>
        private void SetAnimationEvents(AnimationClip clip, AnimationEvent[] animationEvents)
        {
            // The events must be set on the model rather than directly on the clip.
            var modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(clip)) as ModelImporter;
            if (modelImporter == null) {
                return;
            }

            var serializedObject = new SerializedObject(modelImporter);
            var clipAnimationsProperty = serializedObject.FindProperty("m_ClipAnimations");

            if (!clipAnimationsProperty.isArray) {
                return;
            }

            for (int i = 0; i < clipAnimationsProperty.arraySize; ++i) {
                var clipElementProperties = clipAnimationsProperty.GetArrayElementAtIndex(i);
                // Find the matching clip.
                if (clipElementProperties.FindPropertyRelative("name").stringValue == clip.name) {
                    // Update the events through the SerializedProperty.
                    var events = clipElementProperties.FindPropertyRelative("events");
                    events.ClearArray();
                    for (int j = 0; j < animationEvents.Length; ++j) {
                        events.InsertArrayElementAtIndex(j);
                        events.GetArrayElementAtIndex(j).FindPropertyRelative("functionName").stringValue = animationEvents[j].functionName;
                        events.GetArrayElementAtIndex(j).FindPropertyRelative("floatParameter").floatValue = animationEvents[j].floatParameter;
                        events.GetArrayElementAtIndex(j).FindPropertyRelative("intParameter").intValue = animationEvents[j].intParameter;
                        events.GetArrayElementAtIndex(j).FindPropertyRelative("data").stringValue = animationEvents[j].stringParameter;
                        events.GetArrayElementAtIndex(j).FindPropertyRelative("objectReferenceParameter").objectReferenceValue = animationEvents[j].objectReferenceParameter;
                        events.GetArrayElementAtIndex(j).FindPropertyRelative("time").floatValue = animationEvents[j].time;
                    }
                    serializedObject.ApplyModifiedProperties();
                    AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(clip));
                    break;
                }
            }
        }
    }
}