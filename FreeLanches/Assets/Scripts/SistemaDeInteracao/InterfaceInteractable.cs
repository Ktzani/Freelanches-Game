using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceInteractable
{
    public string InteractionPrompt { get; } //Uma forma de fazer um getter
    // public string InteractableType { get; }
    public bool Interact(Interactor interactor, GameObject item = null);
    
}
