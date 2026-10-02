using UnityEngine;

[CreateAssetMenu(fileName ="Cinemachineshaker",menuName ="CinemachineShaker/Profile/allEasyShake")]
public class EasyShakeProfile : ScriptableObject
{
       public void SoftShake()=>Cinemachine_Shaker.instance.Dosoft_Shake(); 
        public void HardShake()=>Cinemachine_Shaker.instance.DoHard_Shake(); 

       public void QuakeShake()=>Cinemachine_Shaker.instance.DoEarthQuake(); 

}
