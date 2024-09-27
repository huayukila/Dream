using Framework.Farm;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataUI : MonoBehaviour
{
    public int ID;
    public Text numberTxt;
    public Text gameTimeTxt;
    public Text realTimeTxt;

    public void Init(int id, SaveDataInfo info)
    {
        numberTxt.text = id.ToString();

        gameTimeTxt.text = info.GameTime;
        realTimeTxt.text = info.RealTime;
    }
}