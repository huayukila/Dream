using System.Collections;
using System.Collections.Generic;
using Framework;
using Framework.Farm;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : SlotUIBase
{
    public Image ItemImg;

    public Text ItemNameTxt;

    public Text PriceTxt;

    public Text NumsTxt;
    private Button buyBtn;

    private void Awake()
    {
        buyBtn = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        buyBtn.onClick.AddListener(OpenBuyWindow);
    }

    public override void UpdateSlotUI()
    {
        if (Data.Nums == 0)
        {
            NumsTxt.text = "0";
            buyBtn.interactable = false;
            return;
        }

        ItemImg.sprite = Data.Item.UIImage;
        ItemNameTxt.text = Data.Item.Name;
        PriceTxt.text = Data.Item.Price.ToString();
        NumsTxt.text = Data.Nums.ToString();
    }


    void OpenBuyWindow()
    {
        FindObjectOfType<ShopManager>().BuyItemWindowCtrl.Open(Data);
    }

    private void OnDestroy()
    {
        buyBtn.onClick.RemoveListener(OpenBuyWindow);
    }
}