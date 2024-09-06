using UnityEngine;

namespace Framework.Farm
{
    public class PlayerController : ProjectCtrler
    {
        public float Speed;
        public SlotsUIManager SlotsUIManager;

        private IPlayerModel _playerModel;
        private IInventorySystem _inventorySystem;
        private float h, v;
        private Transform mainCam;

        private void Start()
        {
            mainCam = Camera.main.transform;
            _playerModel = this.GetModel<IPlayerModel>();
            _inventorySystem = this.GetSystem<IInventorySystem>();
        }

        private void Update()
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            Vector3 temp = h * mainCam.right + v * mainCam.forward;
            transform.position += temp * Speed;
        }
    }
}