using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class Anchor : MonoBehaviour, IInputClickHandler
{

    private bool isSelected = false;
    private Material defaultMat;

    [SerializeField]
    Material selectedMaterial;

    public RoomIdentifier RoomIdentify;
    public WorldAnchorManager worldAnchorManager;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isSelected)
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.Anchor;
            isSelected = true;
            worldAnchorManager.AttachAnchor(RoomIdentify.vr.Transform.parent.gameObject);
        }
        else
        {

            TransformMenu.instance.currentMode = TransformMenu.Mode.None;
            worldAnchorManager.RemoveAnchor(RoomIdentify.vr.Transform.parent.gameObject);
        }
    }


    private void Awake()
    {
        defaultMat = this.gameObject.GetComponent<Renderer>().material;
        RoomIdentify = RoomIdentifier.Instance;
    }

    private void Update()
    {
        TransformMenu.Mode temp = TransformMenu.instance.currentMode;
        if (temp == TransformMenu.Mode.Anchor)
        {
            this.gameObject.GetComponent<Renderer>().material = selectedMaterial;
            isSelected = true;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = defaultMat;
            isSelected = false;
        }
    }
}
