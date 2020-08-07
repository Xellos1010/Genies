using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum eAnimationStates
{
    None = -1,
    Walk,
    Run,
    Wave,
    PickUp,
    backwardsWalk //Need to Remove Hyphens for checks in later scripts
}//future implementation

public class AnimationStateManager : MonoBehaviour
{
    public delegate void AnimationState(string state);
    public static event AnimationState animationStateChanged;

    public Animator animator;
    public string animatorStateToPlay = "";

    public Color backgroundColor1, backgroundColor2, backgroundColor3, backgroundColor4, backgroundColor5;

    internal void PlayAnimation()
    {
        PlayAnimation(animatorStateToPlay);
    }

    public void PlayAnimation(string animation)
    {
        animationStateChanged?.Invoke(animation.Replace("-",string.Empty));
        animator.Play("Base Layer."+animation);
        //TODO Add Event hook to say idle resumed playing and change background color to default
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AnimationStateManager))]
public class PlayAnimationStateEditor : Editor
{
    AnimationStateManager referenceScript;
    public void OnEnable()
    {
        referenceScript = (AnimationStateManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Play Animation via String"))
        {
            referenceScript.PlayAnimation();
        }
    }
}
#endif