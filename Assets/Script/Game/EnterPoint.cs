using Framework.Farm;
using UnityEngine.SceneManagement;

public class EnterPoint : ProjectCtrler
{
    private void Awake()
    {
        UIKit.Init();
        UIRoot.Instance.SetResolution(1920, 1080, 1);
        GetArchitecture();
    }

    private void Start()
    {
        SceneManager.LoadScene("Title");
    }
}