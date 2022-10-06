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

    private ScrollViewButton currentShownCharacter;
    private Tween jumpTween;
    
    public void Show(ScrollViewButton scrollButton)
    {
        jumpTween?.Kill();
        
        currentShownCharacter?.AssociatedItem.InstantiatedGameObject.SetActive(false);

        scrollButton.AssociatedItem.InstantiatedGameObject.SetActive(true);
        scrollButton.AssociatedItem.InstantiatedGameObject.transform.position = jumpStartPoint.position;
        jumpTween = (scrollButton.AssociatedItem.InstantiatedGameObject.transform as RectTransform).DOJump(characterViewPoint.position, jumpPower, 1, jumpDuration);
        jumpTween.onComplete += AdjustPosition;
        jumpTween.Restart();

        currentShownCharacter = scrollButton;
        characterNameText.text = scrollButton.AssociatedItem.Name;
    }
    
    //preventing positioning issues when changing resolution while tween is playing
    private void AdjustPosition()
    {
        currentShownCharacter.AssociatedItem.InstantiatedGameObject.transform.position = characterViewPoint.position;
    }

    private void OnDestroy()
    {
        if(jumpTween != null) jumpTween.onComplete -= AdjustPosition;
    }
}
