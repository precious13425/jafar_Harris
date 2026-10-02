using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_spawner : MonoBehaviour {
	public direction dir;
	 int R;
	public bool spawned;
	Room_Generator rooms;
	float v=0.1f;
	void Start(){
		rooms = Room_Generator.instance;
		Invoke ("Spawn", v);
		Destroy (gameObject, 4);
	}

	public void Spawn(){
		// all doors should gnerate opposite facing rooms as much as possible
		GameObject b=null;
		if (!spawned) {
			switch (dir) {
			case direction.N:
				R = Random.Range (0, rooms.S.Length);
				b = Instantiate (rooms.S [R], transform.position, rooms.S [R].transform.rotation);
				b.transform.SetParent (rooms.transform);

				break;
			case direction.S:
				R = Random.Range (0, rooms.N.Length);
				b = Instantiate (rooms.N [R], transform.position, rooms.N [R].transform.rotation);
				b.transform.SetParent (rooms.transform);

				break;
			case direction.E:
				R = Random.Range (0, rooms.W.Length);
				b = Instantiate (rooms.W [R], transform.position, rooms.W [R].transform.rotation);
				b.transform.SetParent (rooms.transform);

				break;
			case direction.W:
				R = Random.Range (0, rooms.E.Length);
				b = Instantiate (rooms.E [R], transform.position, rooms.E [R].transform.rotation);
				b.transform.SetParent (rooms.transform);

				break;

			default:
				break;
			}
			spawned = true;
		}
	}


	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("SP")) {
			if(col.GetComponent<RM_spawner>().spawned==false && spawned==false){
				Instantiate (rooms.block_room, transform.position, rooms.block_room.transform.rotation,rooms.transform);
				Destroy (col.transform.parent.gameObject);
			}

			//Instantiate (rooms.Door, transform.parent.position, col.transform.parent.localRotation);
			spawned = true;
		}
	}

}
public enum direction{N,S,E,W}