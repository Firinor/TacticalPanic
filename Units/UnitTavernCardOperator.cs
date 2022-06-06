using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitTavernCardOperator : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UnitBasis unit;
    private UnitSprite unitSprite;
    [SerializeField]
    private Image highlighting;
    [SerializeField]
    private Image image;
    private Transform transform;
    private RectTransform rectTransform;
    private Transform parent;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        transform = GetComponent<Transform>();
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent;
        gameObject.SetActive(false);
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
        canvasGroup.blocksRaycasts = false;
        int siblingIndex = transform.GetSiblingIndex();
        SquadOperator.CardOnBeginDrag(transform, siblingIndex);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.localPosition += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        SquadOperator.CardOnEndDrag();
        ReturnToParent();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SquadOperator.instance.RefreshPointsInfo(unit);
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
        gameObject.SetActive(true);

        transform.SetSiblingIndex(unit.id);

        this.unit = unit;
        unitSprite = unit.GetUnitSprite();
        image.sprite = unitSprite.unitFace;
        image.color = Color.white;
    }

    public int GetUnitID()
    {
        return unit.id;
    }
}
