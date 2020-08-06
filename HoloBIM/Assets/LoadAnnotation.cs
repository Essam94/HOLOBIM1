using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.IO;


public class LoadAnnotation : MonoBehaviour, IInputClickHandler
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
    private State state = State.inactive;
    public string[] Names;
    public Vector3[] Locations;
    string filepath ;//This is the path of the text file
    private GameObject objectToBeInstantiated;
    public GameObject prefabObject;
    public GameObject cursor;
    public TextMesh speechToTextOutput;

    public enum State
    {
        active,
        inactive
    }

    // This gets all the locations from the file
    void Start()
    {
        string path = Application.persistentDataPath + "\\" + "AnnotationList.txt";
        string[] filelines = File.ReadAllLines(path);
        Names = new string[filelines.Length];
        Locations = new Vector3[filelines.Length];
        for (int i = 0; i < filelines.Length; i++)
        {
            string[] parts = filelines[i].Split("/"[0]);
            Names[i] = parts[0];
            string coords = parts[1].Substring(1, parts[1].Length - 3);
            string[] coord = coords.Split(","[0]);
            float coord0 = float.Parse(coord[0],System.Globalization.CultureInfo.InvariantCulture);
            float coord1 = float.Parse(coord[1],System.Globalization.CultureInfo.InvariantCulture);
            float coord2 = float.Parse(coord[2],System.Globalization.CultureInfo.InvariantCulture);
            Locations[i] = new Vector3(coord0, coord1, coord2) + RoomIdentifier.Instance.vr.Transform.parent.position;
            Debug.Log(Names[i] + " : " + Locations[i].ToString());
        }
    }


public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isSelected)
        {
            state = State.active;
            isSelected = true;
            
            objectToBeInstantiated = Instantiate(prefabObject);
            objectToBeInstantiated.transform.position = Locations[0];
            objectToBeInstantiated.transform.rotation = Camera.main.transform.rotation;
            objectToBeInstantiated.transform.GetComponent<AnnotateScript>().speechToTextOutput.text = Names[0];


        }
        else
        {

            state = State.inactive;
            isSelected = false;
            objectToBeInstantiated.transform.gameObject.SetActive(false);
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
        if (state == State.active)
        {
            this.gameObject.GetComponent<Renderer>().material = selectedMaterial;
            isSelected = true;
        }
        else if(state == State.inactive)
        {
            this.gameObject.GetComponent<Renderer>().material = defaultMat;
            isSelected = false;
        }
    }
}
