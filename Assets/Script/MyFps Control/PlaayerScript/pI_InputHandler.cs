using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pI_InputHandler : MonoBehaviour
{
      public Player_SO playerdata;

      [Header("Inputs")]
        [SerializeField] string HorizontalInput = "Horizontal";
        [SerializeField] string VerticalInput = "Vertical";


        [Header("Interract call system")]
        public KeyCode useKey;
        [SerializeField]GameEvent onInterract_so;

       

    void Awake()
    {
        playerdata?.NewPlayer (transform);
    }

    float hInput,vInput;

    public  bool  runInput{get;private set;}
        public float GetHorizontal=>hInput;
        public float GetVerical=>vInput;

    // Update is called once per frame
   
    void Update()
    {
      

        hInput = Input.GetAxisRaw(HorizontalInput);
        vInput = Input.GetAxisRaw(VerticalInput);

        //for the interraction
        if (Input.GetKeyDown(useKey) || Input.GetKey(useKey))
        {
          onInterract_so?.OnRaise();
          Debug.LogWarning("First input");
        }

       

                

    }


}
