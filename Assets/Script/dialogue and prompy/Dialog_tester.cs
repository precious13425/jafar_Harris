using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_tester : MonoBehaviour
{
    public string dialogue;
    public GameObject diableobject;

    public Dialogue_Manager.DialogueSystem.Dialogue dialogueM;


    void Start()
    {
        Dialogue_Manager.instance.SetNewdialogue(dialogueM, () =>
        {
            diableobject.SetActive(false);
            Debug.LogWarning("All done");
        });
    }
}
