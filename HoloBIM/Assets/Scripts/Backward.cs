using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class Backward : MonoBehaviour, IInputClickHandler
{

    private bool isSelected = false;
    private Material defaultMat;

    [SerializeField]
    Material selectedMaterial;

    public RoomIdentifier RoomIdentify;



    public void OnInputClicked(InputClickedEventData eventData)
    {

        TransformMenu.instance.currentMode = TransformMenu.Mode.Backward;
        isSelected = true;
        RoomIdentify.vr.Transform.parent.Translate(new Vector3(0, 0, -0.01f));

    }


    private void Awake()
    {
        defaultMat = this.gameObject.GetComponent<Renderer>().material;
        RoomIdentify = RoomIdentifier.Instance;
    }

    private void Update()
    {
        TransformMenu.Mode temp = TransformMenu.instance.currentMode;
        if (temp == TransformMenu.Mode.Backward)
        {
            this.gameObject.GetComponent<Renderer>().material = selectedMaterial;
            isSelected = false;
            this.gameObject.GetComponent<Renderer>().material = defaultMat;
            TransformMenu.instance.currentMode = TransformMenu.Mode.None;
        }

    }
}
