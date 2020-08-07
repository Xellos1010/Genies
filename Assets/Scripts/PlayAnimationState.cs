using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayAnimationState : MonoBehaviour
{
    public Animator animator;
    public string animatorStateToPlay = "";

    internal void PlayAnimation()
    {
        PlayAnimation(animatorStateToPlay);
    }

    public void PlayAnimation(string animation)
    {
        animator.Play("Base Layer."+animation);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayAnimationState))]
public class PlayAnimationStateEditor : Editor
{
    PlayAnimationState referenceScript;
    public void OnEnable()
    {
        referenceScript = (PlayAnimationState)target;
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