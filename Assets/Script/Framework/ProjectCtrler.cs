using Framework;
using UnityEngine;

public class ProjectCtrler : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return Architecture<DreamsFarmProject>.Interface;
    }
}