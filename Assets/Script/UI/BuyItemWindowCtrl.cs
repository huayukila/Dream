using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Farm
{
    public class BuyItemWindowCtrl : ProjectCtrler
    {
        [SerializeField] private Text totalTxt;
        [SerializeField] private Button buyBtn;
        [SerializeField] private Image itemImg;
        [SerializeField] private Slider numsSlider;
        [SerializeField] private TMP_InputField amountInputField;
        [SerializeField] private Button CloseBtn;

        private ISlot cachedSlot;

        int totalCanPutInPackNums = 0;
        int totalCanBuyNums = 0;

        private void Awake()
        {
            amountInputField.onValueChanged.AddListener(OnInputValueChanged);
            numsSlider.onValueChanged.AddListener(OnSliderValueChanged);
            amountInputField.onEndEdit.AddListener(OnOnInputEndEdit);
            CloseBtn.onClick.AddListener(Close);
            buyBtn.onClick.AddListener(OnBuyBtnClick);
            gameObject.SetActive(false);
        }

        public void Open(ISlot data)
        {
            gameObject.SetActive(true);
            totalTxt.text = "0";
            int finalCanBuyNums = 0;
            int canPutInNullSlotNums = 0;
            int canStackNums = 0;
            cachedSlot = data;
            itemImg.sprite = cachedSlot.Item.UIImage;
            var backPack = this.GetModel<IPlayerModel>().BackPack;

            //how many can put in pack
            {
                var sameItemList = this.GetSystem<IInventorySystem>()
                    .FindSameItemAndGetSlotsByItemID(backPack, cachedSlot.Item.ID);

                if (cachedSlot.Item.IsCanStack)
                {
                    foreach (var slot in sameItemList)
                    {
                        canStackNums += slot.Item.MaxStackNums - slot.Nums;
                    }
                }

                canPutInNullSlotNums = backPack.NullSlots.Count * cachedSlot.Item.MaxStackNums;
                totalCanPutInPackNums = canStackNums + canPutInNullSlotNums;
            }

            //how many can buy
            {
            }

            if (totalCanPutInPackNums > cachedSlot.Nums)
            {
                numsSlider.maxValue = cachedSlot.Nums;
            }

            if (totalCanPutInPackNums <= cachedSlot.Nums)
            {
                numsSlider.maxValue = totalCanPutInPackNums;
            }
        }

        void Close()
        {
            totalCanPutInPackNums = 0;
            totalCanBuyNums = 0;
            numsSlider.value = 0;
            amountInputField.text = "0";
            cachedSlot = null;
            gameObject.SetActive(false);
        }

        void OnInputValueChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            int nums = Convert.ToInt32(value);

            int resultNums = 0;
            if (nums > totalCanPutInPackNums)
            {
                resultNums = totalCanPutInPackNums;
            }
            else
            {
                resultNums = nums;
            }

            numsSlider.value = resultNums;
            amountInputField.text = resultNums.ToString();

            totalTxt.text = cachedSlot.Item == null ? "0" : (resultNums * cachedSlot.Item.Price).ToString();
        }

        void OnOnInputEndEdit(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return;

            amountInputField.text = "0";
            numsSlider.value = 0;
        }


        void OnSliderValueChanged(float value)
        {
            int nums = Convert.ToInt32(value);

            int resultNums = 0;
            if (nums > totalCanPutInPackNums)
            {
                resultNums = totalCanPutInPackNums;
            }
            else
            {
                resultNums = nums;
            }

            amountInputField.text = resultNums.ToString();
            totalTxt.text = cachedSlot.Item == null ? "0" : (resultNums * cachedSlot.Item.Price).ToString();
        }

        public void OnBuyBtnClick()
        {
            var backPack = this.GetModel<IPlayerModel>().BackPack;
            int resultNums = Convert.ToInt32(numsSlider.value);

            InventoryUpdateInfo info = new InventoryUpdateInfo()
            {
                TargetUnit = backPack,
                ItemId = cachedSlot.Item.ID,
                Nums = resultNums
            };
            cachedSlot.Nums -= Convert.ToInt32(numsSlider.value);
            this.GetSystem<IInventorySystem>().AddItemToStorageUnit(info);
            this.SendEvent<BoughtItemEvent>();
            Close();
        }

        private void OnDestroy()
        {
            amountInputField.onValueChanged.RemoveListener(OnInputValueChanged);
            numsSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
            buyBtn.onClick.RemoveListener(OnBuyBtnClick);
            amountInputField.onDeselect.RemoveListener(OnOnInputEndEdit);
            CloseBtn.onClick.RemoveListener(Close);
        }
    }
}