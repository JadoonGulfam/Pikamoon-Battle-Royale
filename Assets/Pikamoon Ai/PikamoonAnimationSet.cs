using UnityEngine;
[CreateAssetMenu(fileName = "PikamoonAnimationSet", menuName = "Pikamoon/Animation Set")]
public class PikamoonAnimationSet : ScriptableObject
{
    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip runAnimation;
    public AnimationClip alertAnimation;
    public AnimationClip attackAnimation;
    public AnimationClip stunnedAnimation;
    public AnimationClip killedAnimation;
}
