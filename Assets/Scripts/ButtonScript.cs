using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ButtonScript : MonoBehaviour
{

    // Start is called before the first frame update
    public void OnButtonDown(Hand hand) 
    {
        ColorSelf(Color.cyan);
        hand.TriggerHapticPulse(1000);
    }

    public void OnButtonUp(Hand hand)
    {
        ColorSelf(Color.black);
    }

    private void ColorSelf(Color newColor)
    {
        Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
        renderers[0].material.color = newColor;
        //for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
        //{
        //    renderers[rendererIndex].material.color = newColor;
        //}
    }
}
