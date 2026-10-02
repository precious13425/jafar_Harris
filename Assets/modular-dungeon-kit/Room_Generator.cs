using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
[ExecuteInEditMode]
public class Room_Generator : MonoBehaviour
{

	public static Room_Generator instance;
	void Awake(){
		instance = this;
	}

	public GameObject[] N;
	public GameObject[] S;
	public GameObject[] E;
	public GameObject[] W;
	public GameObject block_room;




}

