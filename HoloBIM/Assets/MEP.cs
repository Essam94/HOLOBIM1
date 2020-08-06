using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MEP : MonoBehaviour, IInputClickHandler
{

    private bool isSelected = false;
    private Material defaultMat;

    [SerializeField]
    Material selectedMaterial;
    public MiniMap MINIMAP;
    public RoomIdentifier RoomIdentify;
    public ScanProgress Progress;
    VirtualRoom Room;
    ObjectManager Manager;
    Transform Rooms;
    Transform FloorPlan;
    public VirtualRoomBehavior[] vRoomBhvrs;
    Vector3[] Corners;
    GameObject Room_Temp;
    Vector3 dimensions;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isSelected)
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.MEP;
            isSelected = true;
            RoomIdentifier.Instance.vr.Transform.parent.Find("M.E.P.").gameObject.SetActive(false);




        }
        else
        {

            TransformMenu.instance.currentMode = TransformMenu.Mode.MEP;
            isSelected = false;
            RoomIdentifier.Instance.vr.Transform.parent.Find("M.E.P.").gameObject.SetActive(true);
        }
    }


    private void Awake()
    {
        defaultMat = this.gameObject.GetComponent<Renderer>().material;
        MINIMAP = MiniMap.Instance;
        RoomIdentify = RoomIdentifier.Instance;
        Rooms = MiniMap.Instance.transform.Find("Rooms");
    }

    private void Update()
    {
        TransformMenu.Mode temp = TransformMenu.instance.currentMode;
        if (temp == TransformMenu.Mode.MEP)
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
