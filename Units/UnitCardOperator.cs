using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCardOperator : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UnitBasis unit;
    private UnitInformator unitInformator;
    [SerializeField]
    private Image highlighting;
    [SerializeField]
    private Image image;
    private RectTransform rectTransform;
    private Transform parent;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Text textComponent;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent;
        textComponent.text = "";
    }

    public void SetParent(Transform parent)
    {
        this.parent = parent;
    }

    public void ReturnToParent()
    {
        transform.SetParent(parent);
        transform.SetSiblingIndex(unit.id);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BlockRaycasts(block: false);
        int siblingIndex = transform.GetSiblingIndex();
        SquadCanvasOperator.CardOnBeginDrag(transform, siblingIndex);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.localPosition += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BlockRaycasts(block: true);
        ReturnToParent();
        SquadCanvasOperator.CardOnEndDrag();
    }

    public void BlockRaycasts(bool block)
    {
        canvasGroup.blocksRaycasts = block;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SquadCanvasOperator.instance.RefreshPointsInfo(unit);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlighting.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlighting.enabled = false;
    }

    public void SetUnit(UnitBasis unit)
    {
        transform.SetSiblingIndex(unit.id);

        this.unit = unit;
        unitInformator = unit.unitInformator;
        image.sprite = unitInformator.unitFace;
        image.color = Color.white;
    }

    public UnitBasis GetUnit()
    {
        return unit;
    }

    public void SetText(string text)
    {
        textComponent.text = text;
    }
}
