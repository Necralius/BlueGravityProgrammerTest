
using UnityEngine.EventSystems;

public class ShopSlot : ItemSlot, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponentInParent<ShopView>())
            GetComponentInParent<ShopView>().InspectItem(item);
        else GetComponentInParent<ShopView>().RemoveInspectionItem();
    }
}