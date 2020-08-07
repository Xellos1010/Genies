using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ImageColorEventManager : MonoBehaviour
{
    internal Dictionary<string, Color> dBackgroundColorReference = new Dictionary<string, Color>() {
    {"Walk",Color.red},
    {"Run", Color.blue},
    {"Wave", Color.green},
    {"PickUp", Color.yellow},
    {"backwardsWalk", Color.cyan}
    };

    public SetImageColor imageColorManager;

    // Start is called before the first frame update
    void Start()
    {
        AnimationStateManager.animationStateChanged += PlayAnimationState_animationStateChanged;
    }

    internal void PlayAnimationState_animationStateChanged(string state)
    {
        Debug.Log("state recieved in color event manager is " + state);
        eAnimationStates stateInvoked;
        if (Enum.TryParse<eAnimationStates>(state, out stateInvoked))
        {
            if (dBackgroundColorReference.Keys.Contains(stateInvoked.ToString()))
                imageColorManager.SetImageColorTo(dBackgroundColorReference[stateInvoked.ToString()]);
            else
                Debug.Log("Dictionary of colors contains no reference to state " + stateInvoked);
        }
        else
        {
            Debug.Log("No Color found for animation " + state + " state not defined in eAnimationStates");
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(ImageColorEventManager))]
public class ImageColorEventManagerEditor : Editor
{
    public ImageColorEventManager targetCache;

    public void OnEnable()
    {
        targetCache = (ImageColorEventManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        List<string> keyList = new List<string>(targetCache.dBackgroundColorReference.Keys);
        for (int i = 0; i < keyList.Count; i++)
        {
            if (GUILayout.Button("Test Animation State " + keyList[i]))
            {
                targetCache.PlayAnimationState_animationStateChanged(keyList[i]);
            }
        }
    }
}

#endif