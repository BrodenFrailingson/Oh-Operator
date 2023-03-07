using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class AuxCord : MonoBehaviour
{

    private GameObject Other, inst;
    [SerializeField] private GameObject m_Ghost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag[0] != 'A' && (other.gameObject.tag[0] != 'U' && other.gameObject.tag[1] != 'L') && other.gameObject.tag[0] != 'F' && other.gameObject.tag[0] != 'G')
            return;
        
        Other = other.gameObject;
        SetGhostPos();

    }
    public string GetTag() => Other ? Other.tag : "Null";
    
    public void SetPosition()
    {
        // tests for non-blank or player related tags, set the position and freeze the ridgid body
        if (Other != null && Other.tag != "MainCamera" && Other.tag != "Player")
        {
            transform.position = Other.transform.position;
            //transform.localPosition = Other.transform.localPosition;
            transform.rotation = Other.transform.root.transform.rotation;
            transform.position -= 0.05f * transform.forward;
            GetComponent<Rigidbody>().isKinematic = true;

            if (inst)
                Destroy(inst);
            
            //Other = null;
        }
    }

    private void SetGhostPos() 
    {
        Transform m_GhostTransform;

        m_GhostTransform = Other.transform;
        m_GhostTransform.transform.rotation = Other.transform.root.transform.rotation; //this can't be legal

        if (inst)
            Destroy(inst);
        inst = Instantiate(m_Ghost, m_GhostTransform);
        inst.transform.localScale = new Vector3(9.0f, 9.0f, 4.64f);
        inst.transform.localPosition -= new Vector3(0, 0, 0.5f);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Other)
            return;
        if (other.gameObject.tag != Other.tag)
            return;
        if (inst)
            Destroy(inst);
        Other = null;
    }

    public void LetGo() 
    {
        if (Other)
        {
            SetPosition();
            return;
        }
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
