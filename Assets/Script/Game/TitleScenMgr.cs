using UnityEngine;

public class TitleScenMgr : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        UIRoot.Instance.SetUICameraActive(true);
    }

    void Start()
    {
        UIKit.OpenPanel("TitlePanel");
    }
}