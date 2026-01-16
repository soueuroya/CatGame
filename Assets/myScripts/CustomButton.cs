using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button
{
    [SerializeField] private List<GameObject> targetObjects = new List<GameObject>();

    public int test;

    private List<Graphic> targetGraphics = new List<Graphic>();

    protected override void Awake()
    {
        base.Awake();
        UpdateTargetGraphics();
    }

    private void UpdateTargetGraphics()
    {
        targetGraphics.Clear();
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                Graphic graphic = obj.GetComponent<Graphic>();
                if (graphic != null)
                {
                    targetGraphics.Add(graphic);
                }
            }
        }
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        // Call the base class transition to apply default behavior
        base.DoStateTransition(state, instant);

        Color targetColor;

        switch (state)
        {
            case SelectionState.Normal:
                targetColor = colors.normalColor;
                break;
            case SelectionState.Highlighted:
                targetColor = colors.highlightedColor;
                break;
            case SelectionState.Pressed:
                targetColor = colors.pressedColor;
                break;
            case SelectionState.Disabled:
                targetColor = colors.disabledColor;
                break;
            default:
                targetColor = Color.white;
                break;
        }

        // Apply the color transition to all target graphics
        foreach (Graphic graphic in targetGraphics)
        {
            if (graphic != null)
            {
                graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
            }
        }
    }
}
