using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

// Custom button: no default color-tint transition, adds hover and right-click support.
[RequireComponent(typeof(Image))]
public class CustomButton : Selectable,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Events")]
    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;

    [Header("Hover Visuals (optional, manual — not the built-in tint)")]
    public bool useCustomHoverVisual = true;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;

    private Image _image;
    private bool _isHovering;
    private bool _isPressed;

    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();

        // Kill the default Selectable color/sprite/animation transition entirely.
        transition = Transition.None;

        if (useCustomHoverVisual && normalSprite != null && _image != null)
            _image.sprite = normalSprite;
    }


    protected override void Reset()
    {
        base.Reset();

        _image = GetComponent<Image>();

        // Kill the default Selectable color/sprite/animation transition entirely.
        transition = Transition.None;
        targetGraphic=image;

        if (useCustomHoverVisual && normalSprite != null && _image != null)
            _image.sprite = normalSprite;

    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactable) return;
        _isHovering = true;
        onHoverEnter?.Invoke();
        UpdateVisual();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        _isPressed = false;
        onHoverExit?.Invoke();
        UpdateVisual();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) return;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isPressed = true;
            UpdateVisual();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        UpdateVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                onLeftClick?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                onRightClick?.Invoke();
                break;
        }
    }

    private void UpdateVisual()
    {
        if (!useCustomHoverVisual || _image == null) return;

        if (_isPressed && pressedSprite != null)
            _image.sprite = pressedSprite;
        else if (_isHovering && hoverSprite != null)
            _image.sprite = hoverSprite;
        else if (normalSprite != null)
            _image.sprite = normalSprite;
    }
}
