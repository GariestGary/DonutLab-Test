using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string showTriggerName;
    
    public void StartShow()
    {
        animator.SetTrigger(showTriggerName);    
    }
}
