using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class NewBehaviourScript : MonoBehaviour, IInputClickHandler
{

    private bool isSelected = false;
    private Material defaultMat;

    [SerializeField]
    Material selectedMaterial;

    public RoomIdentifier RoomIdentify;
 


    public void OnInputClicked(InputClickedEventData eventData)
    {

            TransformMenu.instance.currentMode = TransformMenu.Mode.RotateLeft;
            isSelected = true;
            RoomIdentify.vr.Transform.parent.RotateAround(Vector3.up,0.01f);
     
    }


    private void Awake()
    {
        defaultMat = this.gameObject.GetComponent<Renderer>().material;
        RoomIdentify = RoomIdentifier.Instance;
    }

    private void Update()
    {
        TransformMenu.Mode temp = TransformMenu.instance.currentMode;
        if (temp == TransformMenu.Mode.RotateLeft)
        {
            this.gameObject.GetComponent<Renderer>().material = selectedMaterial;
            isSelected = false;
            this.gameObject.GetComponent<Renderer>().material = defaultMat;
            TransformMenu.instance.currentMode = TransformMenu.Mode.None;
        }
     
    }
}
