using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour
{
    Button curbtn;
    public bool PlayBtnSelect,playBtnAccept;
    void OnEnable()
    {
        curbtn=GetComponent<Button>();
        if(curbtn!=null)
        curbtn.onClick.AddListener(OnclickAction);

    }
    void OnDisable()
    {
         curbtn=GetComponent<Button>();
       if(curbtn!=null) curbtn.onClick.RemoveListener(OnclickAction);
    }

    void OnclickAction()
    {
       if(playBtnAccept)
      AudioManager.instance.audiosystem.playBtnAccept();

       if(PlayBtnSelect)
      AudioManager.instance.audiosystem.PlayBtnSelect();
    }
}
