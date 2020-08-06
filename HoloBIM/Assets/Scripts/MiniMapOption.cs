using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MiniMapOption : MonoBehaviour, IInputClickHandler
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
            TransformMenu.instance.currentMode = TransformMenu.Mode.MiniMap;
            isSelected = true;
            Rooms.transform.gameObject.SetActive(true);
            RoomIdentifier.Instance.vr.Transform.gameObject.SetActive(false);
            // FloorPlan.transform.gameObject.SetActive(false);
            Progress.InstructionTextMesh.text = "MiniMap Activated";
            MINIMAP.setState("MINI_MAP");


        }
        else
        {

            TransformMenu.instance.currentMode = TransformMenu.Mode.MaxiMap;
            Rooms.transform.gameObject.SetActive(false);
            RoomIdentifier.Instance.vr.Transform.gameObject.SetActive(true);
            Room = RoomIdentify.vr;
            RoomIdentify.AlignVirtualAndPhysicalSpace(Room);
            isSelected = false;
            Progress.InstructionTextMesh.text = Room.IdentifyMessage;
            MINIMAP.setState("MAXI_MAP");
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
        if (temp == TransformMenu.Mode.MiniMap)
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
