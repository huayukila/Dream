using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class SlotsUIManager : ProjectCtrler
{
    public GameObject SlotUIPre;
    private List<SlotUI> slotUIs = new List<SlotUI>();
    private IPlayerModel _playerModel;

    private void Start()
    {
        _playerModel = this.GetModel<IPlayerModel>();
        for (int i = 0; i < _playerModel.BackPack.Slots.Length; ++i)
        {
            GameObject obj = Instantiate(SlotUIPre, Vector3.zero, Quaternion.identity, transform);
            SlotUI ui = obj.GetComponent<SlotUI>();
            ui.NameTxt.text = "null";
            ui.NumsTxt.text = "";
            slotUIs.Add(ui);
        }

        UpdateSlots();
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < _playerModel.BackPack.Slots.Length; ++i)
        {
            if (_playerModel.BackPack.Slots[i].Item == null)
            {
                slotUIs[i].NameTxt.text = "null";
                slotUIs[i].NumsTxt.text = "";
                continue;
            }

            slotUIs[i].NameTxt.text = _playerModel.BackPack.Slots[i].Item.Name;
            slotUIs[i].NumsTxt.text = _playerModel.BackPack.Slots[i].Nums.ToString();
        }
    }
}