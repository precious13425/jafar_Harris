using UnityEngine;

public class Shoot_Listner : MonoBehaviour
{
    public Interract_Handler handler;
    //public damageHandler damager;

    public Transform Target;

    void Start()
    {
         handler=Interract_Handler.ins;
    }

    public void DOShoot()
    {
       var tmpdata=handler.curselected;
        Transform target=tmpdata?tmpdata.transform:null;

       handler.OnRangeAction?.Invoke(target);
       
    }


}
