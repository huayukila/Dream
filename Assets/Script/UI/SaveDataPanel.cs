using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Framework.Farm
{
    public enum SaveDataPanelState
    {
        Non,
        Save,
        Load,
        New
    }

    public class SaveOrLoadStateInfo : UIData
    {
        public SaveDataPanelState state;
    }

    public class SaveDataPanel : BasePanel
    {
        public Button CloseBtn;

        public Button[] SaveDatasBtn; //0 is auto safe

        private List<SaveDataUI> _saveDataUis;
        private ISaveSystem _saveSystem;

        void ReLoadSlotInfo()
        {
            var saveDataModel = this.GetModel<ISaveDataModel>();
            foreach (var button in SaveDatasBtn)
            {
                button.onClick.RemoveAllListeners();
                var slotUI = button.GetComponent<SaveDataUI>();
                slotUI.isHadSD = false;
                if (saveDataModel.SaveDataInfos.TryGetValue(slotUI.SlotID, out SaveDataInfo value))
                {
                    slotUI.Init(value);
                }

                button.onClick.AddListener(() => { HandleBtnClick(slotUI.SlotID); });
            }

            foreach (var info in this.GetModel<ISaveDataModel>().SaveDataInfos)
            {
                var buttonUI = _saveDataUis[info.Key];
                var btn = buttonUI.GetComponent<Button>();
                buttonUI.Init(info.Value);
            }
        }

        void HandleBtnClick(int slotID)
        {
            switch ((Info.uiData as SaveOrLoadStateInfo).state)
            {
                case SaveDataPanelState.Load:
                    if (this.GetSystem<ISaveSystem>().Load(slotID))
                    {
                        UIKit.CloseAllPanel(false);
                        SceneManager.LoadScene("Game");
                    }

                    break;
                case SaveDataPanelState.Save:

                    if (_saveDataUis[slotID].isHadSD)
                    {
                        Debug.Log("would you like to overwrite this saveData?");
                    }
                    else
                    {
                        this.GetSystem<ISaveSystem>().Save(slotID);
                    }

                    break;
                case SaveDataPanelState.New:
                    this.GetSystem<ISaveSystem>().CreateNewSaveData(slotID);
                    UIKit.ClosePanel();
                    break;
            }
        }

        protected override void OnEnter()
        {
            ReLoadSlotInfo();
        }

        protected override void OnExit()
        {
            CloseBtn.onClick.RemoveAllListeners();
            foreach (var button in SaveDatasBtn)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnInit()
        {
            CloseBtn.onClick.AddListener(CloseSelf);
            _saveSystem = this.GetSystem<ISaveSystem>();
            _saveDataUis = new List<SaveDataUI>();
            for (int i = 0; i < SaveDatasBtn.Length; i++)
            {
                var ui = SaveDatasBtn[i].GetComponent<SaveDataUI>();
                ui.SlotID = i;
                _saveDataUis.Add(ui);
            }
        }
    }
}