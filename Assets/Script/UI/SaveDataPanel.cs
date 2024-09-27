using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Framework.Farm
{
    public class SaveDataPanel : BasePanel
    {
        public Button CloseBtn;

        public Button[] SaveDatasBtn; //0 is auto safe

        private List<SaveDataUI> _saveDataUis;
        private ISaveSystem _saveSystem;

        void ReLoadSaveDataInfo()
        {
            foreach (var info in this.GetModel<ISaveDataModel>().SaveDataInfos)
            {
                var buttonUI = _saveDataUis[info.Key];
                buttonUI.Init(info.Key, info.Value);
                buttonUI.GetComponent<Button>().onClick.AddListener(() =>
                {
                    this.GetSystem<ISaveSystem>().Load(info.Key);

                    //todo... need fixed
                    UIKit.CloseAllPanel(false);
                    SceneManager.LoadScene("Game");
                    //
                });
            }
        }

        protected override void OnEnter()
        {
            ReLoadSaveDataInfo();
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
                ui.ID = i;
                _saveDataUis.Add(ui);
            }
            foreach (var button in SaveDatasBtn)
            {
                
            }
        }
    }
}