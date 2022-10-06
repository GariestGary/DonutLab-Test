using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StandView : MonoBehaviour, IItemView
{
    [SerializeField] private RectTransform standViewPoint;
    [SerializeField] private RectTransform inPoint;
    [SerializeField] private RectTransform outPoint;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;

    public ScrollViewButton CurrentShownItem { get; private set; }
    private Sequence inTween;
    private Sequence outTween;

    public void Show(ScrollViewButton stand)
    {
        GameObject standObject = CurrentShownItem?.AssociatedItem.InstantiatedGameObject;
        
        if (CurrentShownItem != null)
        {
            outTween?.Kill();
            outTween = DOTween.Sequence();
            (standObject.transform as RectTransform).position = standViewPoint.position;
            outTween.Join((standObject.transform as RectTransform)
                .DOMoveX(outPoint.position.x, duration));
            outTween.Join(standObject.GetComponent<Image>().DOFade(0, duration));
            outTween.Restart();
        }

        CurrentShownItem = stand;

        standObject = CurrentShownItem.AssociatedItem.InstantiatedGameObject;
        
        standObject.SetActive(true);
        RectTransform rect = standObject.transform as RectTransform;
        
        rect.position = 
            new Vector3(inPoint.position.x, standViewPoint.position.y, 0);
        
        inTween?.Kill();
        inTween = DOTween.Sequence();

        inTween.Join(rect.DOMoveX(standViewPoint.position.x, duration)).SetEase(ease);
        inTween.Join(standObject.GetComponent<Image>().DOFade(1, duration));
        inTween.Restart();
    }
    
    
    
}
