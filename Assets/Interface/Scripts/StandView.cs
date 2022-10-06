using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StandView : MonoBehaviour, IItemView
{
    [SerializeField] private RectTransform standViewPoint;
    [SerializeField] private float inOutOffset;
    [SerializeField] private float duration;

    private GameObject currentShownObject;
    private Sequence inTween;
    private Sequence outTween;

    public void Show(ScrollViewButton obj)
    {
        if (currentShownObject != null)
        {
            outTween?.Kill();
            outTween = DOTween.Sequence();
            outTween.Join(currentShownObject.transform.DOMoveX(currentShownObject.transform.position.x + inOutOffset, duration));
            outTween.Restart();
        }

        currentShownObject = obj.AssociatedItem.InstantiatedGameObject;
        currentShownObject.SetActive(true);
        currentShownObject.transform.position = new Vector3(standViewPoint.position.x - inOutOffset, standViewPoint.position.y, 0);
        
        inTween?.Kill();
        inTween = DOTween.Sequence();


        inTween.Join(currentShownObject.transform.DOMoveX(standViewPoint.position.x, duration));
    }
    
    
    
}
