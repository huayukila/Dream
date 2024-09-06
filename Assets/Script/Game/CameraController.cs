using UnityEngine;

namespace Framework.Farm
{
    public class CameraController : MonoBehaviour
    {
        public Transform TargetTrans;

        public float RightOffset;

        public float BackOffset;

        public float UpOffset;

        public float HSpeed;

        public float VSpeed;

        public bool isTest = false;

        private Vector3 rightVec, upVec, backVec;

        private float angle;

        private Vector3 targetToCamDir;

        // Start is called before the first frame update
        void Start()
        {
            
            rightVec = -Vector3.left * RightOffset;
            upVec = Vector3.up * UpOffset;
            backVec = -Vector3.forward * BackOffset;

            angle = Vector3.Angle(rightVec + backVec, backVec);
            Vector3 temp = new Vector3(transform.position.x, 0, transform.position.z);
            targetToCamDir = temp - TargetTrans.position;
            Init();
        }

        void Init()
        {
            transform.position = TargetTrans.position + rightVec + upVec + backVec;
            transform.rotation = TargetTrans.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (isTest)
            {
                Init();
            }

            float horizontal = Input.GetAxis("Mouse X");
            if (Mathf.Abs(horizontal) > 0.01f)
            {
                targetToCamDir = Quaternion.Euler(0, horizontal * HSpeed, 0) * targetToCamDir;
            }
        }

        private void LateUpdate()
        {
            transform.position = TargetTrans.position + targetToCamDir + upVec;
            transform.rotation = Quaternion.LookRotation(Quaternion.Euler(0, angle, 0) * (-targetToCamDir));
        }
    }
}