using UnityEngine;

namespace Framework.Farm
{
    public class PlayerController : ProjectCtrler
    {
        public float Speed;

        private float h, v;
        private Transform mainCam;

        private bool isOpenBackPack = false;

        private void Start()
        {
            mainCam = Camera.main.transform;
        }

        private void Update()
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            Vector3 temp = h * mainCam.right + v * mainCam.forward;
            transform.position += temp * Speed * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!isOpenBackPack)
                {
                    UIKit.OpenPanel("BackPackPanel");
                    isOpenBackPack = true;
                }
                else
                {
                    UIKit.ClosePanel();
                    isOpenBackPack = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                this.GetSystem<ISaveSystem>().Save();
            }
        }
    }
}