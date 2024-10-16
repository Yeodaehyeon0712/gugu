using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="AnimationContainer",menuName = "Data/AnimationContainer", order =int.MinValue)]
public class AnimatorContainer : ScriptableObject
{
    public RuntimeAnimatorController Animator;
}
