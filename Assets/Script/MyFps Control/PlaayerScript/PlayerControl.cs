using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
   public enum PlayerStates
    {
        Idle,
        Walking,
        Running,
        Jumping
    }
    
       #region  Varaibles
        public PlayerStates playerStates;
        public Gamdata_SO gamedata;

        [Header("Inputs")]
        public pI_InputHandler inputHandler;
        float hInput ;
        float vInput ; 
        [SerializeField]float _speed;
        [SerializeField]bool isrunning;


        

        [Header("Player Motor")]
        CharacterController characterController;
        
        [Range(1f,15f)]
        public float walkSpeed;
        [Range(1f,15f)]
        public float runSpeed;
        [Range(1f,15f)]
        public float JumpForce;
        public Transform FootLocation;

        public float accelrationrate,decelrate;

        [Header("Animator and Parameters")]
        [SerializeField] Animator CharacterAnimator;
        [SerializeField] bool JumpAnimation;
        [SerializeField] bool LandAnimation;
         float HorzAnimation;
         float VertAnimation;
         

        [Header("Sounds")]
        public FootStepHandler footStepHandler;
        // public List<AudioClip> FootstepSounds;
        // public List<AudioClip> JumpSounds;
        // public List<AudioClip> LandSounds;


// handle the footstop sound and delay
       [SerializeField] float _footstepDelay;
        AudioSource _audioSource;
        float footstep_et = 0;

#endregion


        // Use this for initialization
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            _audioSource = gameObject.AddComponent<AudioSource>();
            gamedata.ResumeGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (!gamedata.isPlay)
            
                return;
            
            //handle input
            HandleInput();

            //handle controller
            HandlePlayerControls();

            //handlePlayerState
            HandlePlayerState();

            //sync animations with controller
            SetCharacterAnimations();

            //sync footsteps with controller
            PlayFootstepSounds();
        }

        void HandleInput()
        {
            if(!inputHandler)
            return;
            vInput=inputHandler.GetVerical;
            hInput=inputHandler.GetHorizontal;
            isrunning=inputHandler.runInput;
        }

        void HandlePlayerControls()
        {
           
            Vector3 fwdMovement = characterController.isGrounded == true ? transform.forward * vInput : Vector3.zero;
            Vector3 rightMovement = characterController.isGrounded == true ? transform.right * hInput : Vector3.zero;

                _speed=hInput!=0||vInput!=0?Mathf.MoveTowards(_speed,walkSpeed,accelrationrate*Time.deltaTime):
                    Mathf.MoveTowards(_speed,0,decelrate*Time.deltaTime);

             _speed=Mathf.Clamp(_speed,0,walkSpeed);
            characterController.SimpleMove(Vector3.ClampMagnitude(fwdMovement + rightMovement, 1f) * _speed);

          
        }


        void HandlePlayerState()
        {
              //Managing Player States
            if (characterController.isGrounded)
            {
                if (hInput == 0 && vInput == 0)
                    playerStates = PlayerStates.Idle;
                else
                {
                   
                        playerStates = PlayerStates.Walking;
                   

                    _footstepDelay = (2/_speed);
                }
            }
            else
                playerStates = PlayerStates.Jumping;
        }

#region  Un_needed __n
//         void Jump()
//         {
//            /* if (Input.GetButtonDown(JumpInput))
//             {
//                 StartCoroutine(PerformJumpRoutine());
//                 JumpAnimation = true;
//             }*/
//         }

    //    IEnumerator PerformJumpRoutine()
    //     {
    //         //play jump sound
    //         if (_audioSource)
    //             _audioSource.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Count)]);

    //         float _jump = JumpForce;

    //         do
    //         {
    //             characterController.Move(Vector3.up * _jump * Time.deltaTime);
    //             _jump -= Time.deltaTime;
    //             yield return null;
    //         }
    //         while (!characterController.isGrounded);

    //         //play land sound
    //         if (_audioSource)
    //             _audioSource.PlayOneShot(LandSounds[Random.Range(0, LandSounds.Count)]);

    //     }
   bool onGround()
        {
            bool retVal = false;

            if (Physics.Raycast(FootLocation.position, Vector3.down, 0.1f))
                retVal = true;
            else
                retVal = false;

            return retVal;
        }

#endregion
       
    void SetCharacterAnimations()
        {
            if (!CharacterAnimator)
                return;

            switch (playerStates)
            {
                case PlayerStates.Idle:
                    HorzAnimation = Mathf.Lerp(HorzAnimation, 0, 5 * Time.deltaTime);
                    VertAnimation = Mathf.Lerp(VertAnimation, 0, 5 * Time.deltaTime);
                    break;

                case PlayerStates.Walking:
                    HorzAnimation = Mathf.Lerp(HorzAnimation, 1 * Input.GetAxis("Horizontal"), 5 * Time.deltaTime);
                    VertAnimation = Mathf.Lerp(VertAnimation, 1 * Input.GetAxis("Vertical"), 5 * Time.deltaTime);
                    break;

                case PlayerStates.Running:
                    HorzAnimation = Mathf.Lerp(HorzAnimation, 2 * Input.GetAxis("Horizontal"), 5 * Time.deltaTime);
                    VertAnimation = Mathf.Lerp(VertAnimation, 2 * Input.GetAxis("Vertical"), 5 * Time.deltaTime);
                    break;

                case PlayerStates.Jumping:
                    if (JumpAnimation)
                    {
                        CharacterAnimator.SetTrigger("Jump");
                        JumpAnimation = false;
                    }
                    break;
            }

            LandAnimation = characterController.isGrounded;
            CharacterAnimator.SetFloat("Horizontal", HorzAnimation);
            CharacterAnimator.SetFloat("Vertical", VertAnimation);
            CharacterAnimator.SetBool("isGrounded", LandAnimation);
        }
   
        void PlayFootstepSounds()
        {
            if (playerStates == PlayerStates.Idle || playerStates == PlayerStates.Jumping)
                return;

            footStepHandler?.PlayFootstepSounds(_footstepDelay);

            
        }

    

    

    
}
