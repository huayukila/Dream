using UnityEngine;

namespace Framework.Farm
{
    public interface ICameraControllerSetting
    {
        ICameraControllerSetting SetFov(float value);
    }
    
    public class CameraController : MonoBehaviour, ICameraControllerSetting
    {
        public Transform TargetTrans;

        public float RightOffset;

        public float BackOffset;

        public float UpOffset;

        public float HSpeed;

        public float VSpeed;

        private Vector3 fstVec;

        private Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
            fstVec = TargetTrans.right * RightOffset;

            Vector3 secVec = Quaternion.Euler(0, 90, 0) * fstVec.normalized * BackOffset;

            Vector3 trdVec = TargetTrans.up * UpOffset;

            transform.position = TargetTrans.position + fstVec + secVec + trdVec;
        }

        // Update is called once per frame
        void Update()
        {
            fstVec = fstVec.normalized * RightOffset;
            float horizontal = Input.GetAxis("Mouse X");
            if (Mathf.Abs(horizontal) > 0.01f)
            {
                fstVec = Quaternion.Euler(0, horizontal * HSpeed, 0) * fstVec;
            }
        }

        private void LateUpdate()
        {
            Vector3 secVec = Quaternion.Euler(0, 90, 0) * fstVec.normalized * BackOffset;

            Vector3 trdVec = TargetTrans.up * UpOffset;

            Vector3 temp = TargetTrans.position + fstVec + secVec + trdVec;

            transform.position = Vector3.Lerp(temp, transform.position, 0.1f);
            transform.rotation = Quaternion.LookRotation(-new Vector3(secVec.x, 0, secVec.z));
        }

        public ICameraControllerSetting SetFov(float value)
        {
            if (value is > 110 or < 0)
            {
                Debug.Log("Fov should between 0-110");
            }

            cam.fieldOfView = Mathf.Clamp(value, 0, 110);
            return this;
        }
    }
}