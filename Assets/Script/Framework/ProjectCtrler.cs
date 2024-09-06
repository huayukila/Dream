using Framework;
using UnityEngine;

namespace Framework.Farm
{
    public class ProjectCtrler : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return Architecture<DreamsFarmProject>.Interface;
        }
    }
}