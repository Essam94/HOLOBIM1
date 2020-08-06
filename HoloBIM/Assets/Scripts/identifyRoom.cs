using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.SceneManagement;

public class identifyRoom : MonoBehaviour, IInputClickHandler
{

    private bool isSelected = false;
    private Material defaultMat;

    [SerializeField]
    Material selectedMaterial;
    public RoomIdentifier RoomIdentify;
    GameObject Clone;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isSelected)
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.Identify;
            isSelected = true;
            RoomIdentify.gameObject.SetActive(false);

 

        }
        else
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.None;
            isSelected = false;

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
        if (temp == TransformMenu.Mode.Identify)
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
