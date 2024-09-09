using System.Collections.Generic;
using Framework.Farm.Utils;
using UnityEngine;

namespace Framework.Farm
{
    public class BackPackPanel : BasePanel
    {
        public GameObject Grid;
        
        private List<SlotUI> uiSlots;

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnInit()
        {
            this.RegisterEvent<BoughtItemEvent>(e => { UpdateSlots(); }).UnregisterWhenGameObjectDestroyed(gameObject);
            var playerModel = this.GetModel<IPlayerModel>();
            uiSlots = UISlotHelper.Instance.AllocateUISlots(playerModel.BackPack.UnitSize,
                Grid.transform);

            playerModel = this.GetModel<IPlayerModel>();
            for (int i = 0; i < playerModel.BackPack.Slots.Count; ++i)
            {
                uiSlots[i].InitWithData(playerModel.BackPack.Slots[i]);
            }
        }

        public void UpdateSlots()
        {
            for (int i = 0; i < this.GetModel<IPlayerModel>().BackPack.Slots.Count; ++i)
            {
                uiSlots[i].UpdateSlotUI();
            }
        }
    }
}