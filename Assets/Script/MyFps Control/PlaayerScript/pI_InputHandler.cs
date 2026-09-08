using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class pI_InputHandler : MonoBehaviour
{

    [Header("Inputs")]
    [SerializeField] string HorizontalInput = "Horizontal";
    [SerializeField] string VerticalInput = "Vertical";


    [Header("Interract call system")]
    public KeyCode useKey,switchkey;
    


    float hInput,vInput;

    public  bool  runInput{get;private set;}
    public float GetHorizontal=>hInput;
    public float GetVerical=>vInput;

    // Update is called once per frame

    void Start()
    {
        SwitchWepon();
    }

    void Update()
    {
      

        hInput = Input.GetAxisRaw(HorizontalInput);
        vInput = Input.GetAxisRaw(VerticalInput);

        //for the interraction
        if (Input.GetKeyDown(useKey) || Input.GetKey(useKey))
        {
          activeWepon_so?.OnRaise();
          Debug.LogWarning("First input");
        }

        if (Input.GetKeyDown(switchkey))
        {
          Wepon_switch_So?.OnRaise();
          Debug.LogWarning("First input");
          weponSwitch_Event.Invoke();
          gunactive=!gunactive;
          SwitchWepon();
        }

    } 



    [Header("Wepon Switching")]
     [SerializeField]GameEvent activeWepon_so,Wepon_switch_So;
    [SerializeField]GameObject gunModel,AxeModel;
    [SerializeField] bool gunactive;
    [SerializeField]GameEvent gun_event,axe_event;
    public UnityEvent weponSwitch_Event;
     
    public void SwitchWepon()
    {
        gunModel?.SetActive(false);
        AxeModel?.SetActive(false);

        if (gunactive)
        {
            activeWepon_so=gun_event;
            gunModel?.SetActive(true);
        }
        else
        {
            activeWepon_so=axe_event;
            AxeModel?.SetActive(true);
        }
    }
       
                

    


}
