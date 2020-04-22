using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 一个高度封装的 Hover 交互效果组件
/// </summary>
public class HoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /**
     * 是否在 hover 时把控件调整到最后渲染，使它显示在最上层
     * 通过附加 Canvas 和 GraphicRaycaster 组件实现，动态调整 Canvas 的 sortingOrder 属性
     */
    public bool topWhenHover;
    // topWhenHover 为 true 时，使用此值设置 canvas 组件的 sortingOrder 属性
    public int sortingOrder = 1;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.AddComponent<Animator>();
        var controller = Resources.Load<RuntimeAnimatorController>("Animations/ScaleWhenHover");
        animator.runtimeAnimatorController = Instantiate(controller);
        if (topWhenHover)
        {
            if (GetComponent<Canvas>() == null)
            {
                gameObject.AddComponent<Canvas>();
            }
            if (GetComponent<GraphicRaycaster>() == null)
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }
        }
    }

    private void OnHoverChanged(bool hover)
    {
        if (topWhenHover)
        {
            var canvas = GetComponent<Canvas>();
            canvas.overrideSorting = hover;
            canvas.sortingOrder = sortingOrder;
        }
        animator.SetBool("Hovering", hover);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverChanged(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverChanged(false);
    }
}
