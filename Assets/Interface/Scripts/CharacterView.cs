using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterView : MonoBehaviour, IItemView
{
    [SerializeField] private ContentHandler content;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [Header("Animation Parameters")]
    [SerializeField] private RectTransform characterViewPoint;
    [SerializeField] private RectTransform jumpStartPoint;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDuration;
    [SerializeField] private string jumpTriggerName;

    public ScrollViewButton CurrentShownItem { get; private set; }
    private Tween jumpTween;
    
    public void Show(ScrollViewButton scrollButton)
    {
        jumpTween?.Kill();
        
        CurrentShownItem?.AssociatedItem.InstantiatedGameObject.SetActive(false);

        scrollButton.AssociatedItem.InstantiatedGameObject.SetActive(true);
        (scrollButton.AssociatedItem.InstantiatedGameObject.transform as RectTransform).position = jumpStartPoint.position;
        jumpTween = (scrollButton.AssociatedItem.InstantiatedGameObject.transform as RectTransform)
            .DOJump(characterViewPoint.position, jumpPower, 1, jumpDuration).SetEase(Ease.Linear);
        jumpTween.onComplete += AdjustPosition;
        jumpTween.Restart();

        CurrentShownItem = scrollButton;

        CurrentShownItem.AssociatedItem.InstantiatedGameObject.GetComponent<Animator>().SetTrigger(jumpTriggerName);
        characterNameText.text = scrollButton.AssociatedItem.Name;
    }
    
    //preventing positioning issues when changing resolution while tween is playing
    private void AdjustPosition()
    {
        (CurrentShownItem.AssociatedItem.InstantiatedGameObject.transform as RectTransform).position = characterViewPoint.position;
    }

    private void OnDestroy()
    {
        if(jumpTween != null) jumpTween.onComplete -= AdjustPosition;
    }
}
