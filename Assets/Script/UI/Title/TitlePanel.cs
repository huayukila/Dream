using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Framework.Farm
{
    public class TitlePanel : BasePanel
    {
        public Button newGameBtn, loadBtn, configBtn, existBtn;

        private void OnDestroy()
        {
            newGameBtn.onClick.RemoveAllListeners();
            loadBtn.onClick.RemoveAllListeners();
            configBtn.onClick.RemoveAllListeners();
            existBtn.onClick.RemoveAllListeners();
        }

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
            newGameBtn.onClick.AddListener(() => { this.GetSystem<ISaveSystem>().CreateNewSaveData(); });
            loadBtn.onClick.AddListener(() =>
            {
                UIKit.OpenPanel("SaveDataPanel");
            });
        }
    }
}