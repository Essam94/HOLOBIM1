using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class pos : MonoBehaviour, IInputClickHandler
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
    GameObject Axis;
    Vector3 tempPosition;
    Quaternion tempRotation;
    private State state = State.inactive;
    public enum State
    {
        active,
        inactive
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isSelected)
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.Position;

            state = State.active;
            isSelected = true;
            Axis.SetActive(true);
            RoomIdentify.vr.Transform.parent.gameObject.SetActive(false);
            Rooms.transform.gameObject.SetActive(false);
            ScanProgress.Instance   .InstructionTextMesh.text = string.Format("Please Move Axis to desired Location");




        }
        else
        {
            TransformMenu.instance.currentMode = TransformMenu.Mode.Position;

            state = State.inactive;
            isSelected = false;
            Axis.SetActive(false);
            RoomIdentify.vr.Transform.parent.gameObject.SetActive(true);
            Rooms.transform.gameObject.SetActive(true);
            ScanProgress.Instance.InstructionTextMesh.text = string.Format(RoomIdentifier.Instance.vr.IdentifyMessage);
            RoomIdentifier.Instance.vr.Transform.parent.localPosition = tempPosition;
            RoomIdentifier.Instance.vr.Transform.parent.rotation = tempRotation;
        }
    }


    private void Awake()
    {
        defaultMat = this.gameObject.GetComponent<Renderer>().material;
        MINIMAP = MiniMap.Instance;
        RoomIdentify = RoomIdentifier.Instance;
        Rooms = MiniMap.Instance.transform.Find("Rooms");
        Axis = MiniMap.Instance.transform.Find("axis").gameObject;
    }

    private void Update()
    {
        tempPosition = Axis.gameObject.transform.position;
        tempRotation = Axis.gameObject.transform.localRotation;
        TransformMenu.Mode temp = TransformMenu.instance.currentMode;
        if (state == State.active)
        {
            this.gameObject.GetComponent<Renderer>().material = selectedMaterial;
            isSelected = true;
        }
        else if (state == State.inactive)
        {
            this.gameObject.GetComponent<Renderer>().material = defaultMat;
            isSelected = false;
        }
    }
}
