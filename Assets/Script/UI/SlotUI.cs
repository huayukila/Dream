using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Framework.Farm
{
    public class SlotUI : SlotUIBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image ItemImg;
        [SerializeField] private Text NameTxt;
        [SerializeField] private Text NumsTxt;
        [SerializeField] private GameObject slotObj;
        private Vector3 prePos;
        private bool mDraging = false;

        private Image slotImg;

        private Transform originalRoot;

        private void Start()
        {
            slotImg = GetComponent<Image>();
            originalRoot = transform.parent; //ÇµÇŒÇÁÇ≠parent,ç°å„parent.parentÇÃâ¬î\ê´Ç™Ç†ÇÈ
        }

        public override void UpdateSlotUI()
        {
            //todo... need ReConstruct
            if (Data.Nums == 0)
            {
                NameTxt.text = "";
                NumsTxt.text = "";
                ItemImg.sprite = null;
                return;
            }

            NameTxt.text = Data.Item.Name;
            NumsTxt.text = Data.Nums.ToString();
            ItemImg.sprite = Data.Item.UIImage;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Data.Item == null)
                return;
            mDraging = true;
            prePos = slotObj.transform.position;
            SyncMouse();
            slotImg.raycastTarget = false;
            slotObj.transform.SetParent(originalRoot);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!mDraging)
                return;
            SyncMouse();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!mDraging)
                return;
            GameObject pointingObj = eventData.pointerCurrentRaycast.gameObject;
            if (pointingObj != null)
            {
                if (pointingObj.CompareTag("Slot"))
                {
                    var uiSlot = pointingObj.GetComponent<SlotUI>();
                    this.GetSystem<IInventorySystem>().DragItemToSlot(this, uiSlot);
                    FindObjectOfType<SlotsUIManager>().UpdateSlots();
                }
            }

            slotObj.transform.position = prePos;
            prePos = Vector3.zero;
            slotImg.raycastTarget = true;
            mDraging = false;
            slotObj.transform.SetParent(transform);
        }

        void SyncMouse()
        {
            var mousePosition = Input.mousePosition;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(),
                    mousePosition, null, out var worldPoint))
            {
                slotObj.transform.position = worldPoint;
            }
        }
    }
}