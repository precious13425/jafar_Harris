using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps_CamControl : MonoBehaviour
{
   
   #region  Variables
   
        [Header("Character Animator")]
       [SerializeField] Transform FPSController;
       Transform _fpsCameraHelper;
        public Animator CharacterAnimator;
        public Transform headTransform;

        [Header("Input Settings")]
        public string MouseXInput;
        public string MouseYInput;

        [Header("Common Camera Settings")]
        public Vector3 FPS_CameraOffset;
        public Vector2 FPS_MinMaxAngles;
        public float mouseSensitivity;
        float xClamp;

        #endregion

 
        private void Awake()
        {
            xClamp = 0;
        }

        // Use this for initialization
        void Start()
        {
            if (CharacterAnimator)
            {
                Add_FPSCamPositionHelper();
            }
            else if (headTransform)
            {
                SetHeadPos();
            }
        }

        void Add_FPSCamPositionHelper()
        {
            _fpsCameraHelper = new GameObject().transform;
            _fpsCameraHelper.name = "_fpsCameraHelper";
            _fpsCameraHelper.SetParent(CharacterAnimator.GetBoneTransform(HumanBodyBones.Head));
            _fpsCameraHelper.localPosition = Vector3.zero;
        }

        void SetHeadPos()
        {
            _fpsCameraHelper = new GameObject().transform;
            _fpsCameraHelper.name = "_fpsCameraHelper";
            _fpsCameraHelper.SetParent(headTransform);
            _fpsCameraHelper.localPosition = Vector3.zero; 
        }


       

        // Update is called once per frame
        void Update()
        {
           
            RotateCamera();
            SetCameraHelperPosition_FPS();

        }


          void SetCameraHelperPosition_FPS()
        {
            if (!CharacterAnimator)
                return;

            _fpsCameraHelper.localPosition = FPS_CameraOffset;

            transform.position = _fpsCameraHelper.position;

        }

        void RotateCamera()
        {
            //get mouse delta input
            float mouseX = Input.GetAxis(MouseXInput) * (mouseSensitivity * Time.deltaTime);
            float mouseY = Input.GetAxis(MouseYInput) * (mouseSensitivity * Time.deltaTime);

            //get the euler rotaton of the camera ransform
            Vector3 eulerRotation = transform.eulerAngles;


            //apply the input and clamp it to euler vector
            xClamp += mouseY;
            xClamp = Mathf.Clamp(xClamp, FPS_MinMaxAngles.x, FPS_MinMaxAngles.y);
            eulerRotation.x = -xClamp;
           

            //apply the UP DOWN camera rotation to the camera transform
            transform.eulerAngles = eulerRotation;

            //applys the LEFT RIGHT rotation to the player object
            //also acts as root of the system
            FPSController.Rotate(Vector3.up * mouseX);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (_fpsCameraHelper)
                Gizmos.DrawWireSphere(_fpsCameraHelper.position, 0.1f);

        }
    
}