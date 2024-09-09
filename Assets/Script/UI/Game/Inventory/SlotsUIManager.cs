using System.Collections.Generic;
using UnityEngine;

namespace Framework.Farm
{
    public class SlotsUIManager : ProjectCtrler
    {
        public GameObject SlotUIPre;
        private List<SlotUI> slotUIs = new List<SlotUI>();
        private IPlayerModel _playerModel;

        private void Awake()
        {
            _playerModel = this.GetModel<IPlayerModel>();
            for (int i = 0; i < _playerModel.BackPack.Slots.Count; ++i)
            {
                GameObject obj = Instantiate(SlotUIPre, Vector3.zero, Quaternion.identity, transform);
                SlotUI ui = obj.GetComponent<SlotUI>();
                ui.InitWithData(_playerModel.BackPack.Slots[i]);
                slotUIs.Add(ui);
            }
        }

        private void Start()
        {
            
        }


    }
}