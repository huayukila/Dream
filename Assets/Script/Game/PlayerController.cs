using UnityEngine;

namespace Framework
{
    public class PlayerController : ProjectCtrler
    {
        public SlotsUIManager SlotsUIManager;

        private IPlayerModel _playerModel;
        private IStorageSystem _storageSystem;

        private void Start()
        {
            _playerModel = this.GetModel<IPlayerModel>();
            _storageSystem = this.GetSystem<IStorageSystem>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _storageSystem.AddItemToStorageUnit(new StorageUpdateInfo()
                {
                    Item = new Item(01, "boom", 100),
                    TargetUnit = _playerModel.BackPack,
                    Nums = 80
                });
                SlotsUIManager.UpdateSlots();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _storageSystem.AddItemToStorageUnit(new StorageUpdateInfo()
                {
                    Item = new Item(02, "ass", 100),
                    TargetUnit = _playerModel.BackPack,
                    Nums = 20
                });
                SlotsUIManager.UpdateSlots();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _storageSystem.RemoveItemFromStorageUnit(new StorageUpdateInfo()
                {
                    Item = new Item(02, "ass", 100),
                    TargetUnit = _playerModel.BackPack,
                    Nums = 10
                });
                SlotsUIManager.UpdateSlots();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _storageSystem.RemoveItemFromStorageUnit(new StorageUpdateInfo()
                {
                    Item = new Item(01, "boom", 100),
                    TargetUnit = _playerModel.BackPack,
                    Nums = 20
                });
                SlotsUIManager.UpdateSlots();
            }
        }
    }
}