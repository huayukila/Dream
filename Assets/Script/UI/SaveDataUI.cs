using Framework.Farm;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataUI : MonoBehaviour
{
    public int SlotID;
    public Text numberTxt;
    public Text gameTimeTxt;
    public Text realTimeTxt;

    //is it had saveData
    public bool isHadSD = false;

    public void Init(SaveDataInfo info)
    {
        numberTxt.text = SlotID.ToString();
        gameTimeTxt.text = info.GameTime;
        realTimeTxt.text = info.RealTime;
        isHadSD = true;
    }
}