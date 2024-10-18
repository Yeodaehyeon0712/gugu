using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="SOAnimationController",menuName = "ScriptableObject/SOAnimationController", order =int.MinValue)]
public class SOAnimatorController : ScriptableObject
{
    public RuntimeAnimatorController Animator;
}
