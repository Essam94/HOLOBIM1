using HoloToolkit.Unity.InputModule;
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class AnnotateScript : MonoBehaviour, IDictationHandler, IInputClickHandler
{
    //Initialization variables
    public GameObject objectToBeInstantiated;
    public GameObject prefabObject;
    public GameObject cursor;
    public TextMesh speechToTextOutput;
    private AudioSource audioSource;
    private AudioClip dictationAudioClip;
    private bool isRecording;
    private bool audioState;
    public String content = " ";
    private void Awake()
    {
        dictationAudioClip = GetComponent<AudioClip>();
        audioSource = GetComponent<AudioSource>();
        isRecording = false;
        audioState = false;
    }
    public void Update()
    {
        if (audioSource != null && !audioSource.isPlaying && audioState)
        {
            audioSource.Stop();
            audioState = false;
            return;
        }
        
    }
    public void AnnotateOnSpeech()
    {
        //Instantiation code
        objectToBeInstantiated = Instantiate(prefabObject, cursor.transform.position, Camera.main.transform.rotation);
        objectToBeInstantiated.transform.position = cursor.transform.position; // Instantiate object at cursor position

        audioSource = objectToBeInstantiated.GetComponent<AudioSource>();
    }
    //Methods that need to be implemented fór the IDictationHandler
    public void OnDictationComplete(DictationEventData eventData)
    {
        speechToTextOutput.text = eventData.DictationResult;
        speechToTextOutput.color = Color.green;
        dictationAudioClip = eventData.DictationAudioClip;
        audioSource.clip = dictationAudioClip;
        
        float xPos = objectToBeInstantiated.transform.position.x - RoomIdentifier.Instance.vr.Transform.parent.position.x;
        float yPos = objectToBeInstantiated.transform.position.y - RoomIdentifier.Instance.vr.Transform.parent.position.y;
        float zPos = objectToBeInstantiated.transform.position.z - RoomIdentifier.Instance.vr.Transform.parent.position.z;
        content = speechToTextOutput.text + "/" + "(" + xPos + "," + yPos + "," + zPos + ")";
        SaveAnnotationList(content, "AnnotationList.txt");
    }


    public void OnDictationError(DictationEventData eventData)
    {
        speechToTextOutput.text = eventData.DictationResult;
        speechToTextOutput.color = Color.white;
        isRecording = false;
        StartCoroutine(DictationInputManager.StopRecording());
    }
    public void OnDictationHypothesis(DictationEventData eventData)
    {
        speechToTextOutput.text = eventData.DictationResult;
    }
    public void OnDictationResult(DictationEventData eventData)
    {
        speechToTextOutput.text = eventData.DictationResult;
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        //check if tapped object's child's Textmesh is not null
        if (eventData.selectedObject.GetComponentInChildren<TextMesh>() == null) { Debug.Log("Text mesh is null"); }
        if (eventData.selectedObject.GetComponentInChildren<TextMesh>().color == Color.green)
        {
            audioSource.Stop();
            audioState = false;
            PlayAudio();
        }
        else
        {
            Recording();
        }
    }
    public void PlayAudio()
    {
        if (audioSource == null || audioSource.isPlaying)
        {
            Debug.Log("Audio source is null or is playing");
            return;
        }
        audioSource.Play();
        audioState = true;
    }
    private void Recording()
    {
        if (isRecording)
        {
            isRecording = false;
            StartCoroutine(DictationInputManager.StopRecording());
            speechToTextOutput.color = Color.white;

        }
        else
        {
            isRecording = true;
         StartCoroutine(DictationInputManager.StartRecording( 5f, 20f, 10));
            speechToTextOutput.color = Color.red;
        }
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }


    public static void SaveAnnotationList(string content, string nameList)
    {

        
        try
        {

            string path = Application.persistentDataPath + "\\" + nameList;
            StreamWriter writer = File.AppendText(path);
            writer.WriteLine();
            writer.Flush();
            writer.Close();
            writer.Dispose();
            if (File.Exists(path)) {
                using (FileStream aFile = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    AddText(aFile, content);
                }
            }
            else
            {


                using (FileStream fs = File.Create(path))
                {
                    AddText(fs, content);
                }
            }
        }

        catch (System.Exception ex)
        {
            Debug.LogError("Save CodeList List Error: " + ex.ToString());
        }

    }
  
}