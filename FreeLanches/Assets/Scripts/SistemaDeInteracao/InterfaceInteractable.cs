using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceInteractable
{
    public string InteractionPrompt { get; } //Uma forma de fazer um getter
    public bool Interact(Interactor interactor);
}
