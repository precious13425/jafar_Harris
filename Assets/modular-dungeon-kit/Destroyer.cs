using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{

	void OnTriggerEnter(Collider col){
		if(col.CompareTag("SP"))
		Destroy (col.transform.parent.gameObject);
		Destroy (gameObject, 4f);
	}
}

