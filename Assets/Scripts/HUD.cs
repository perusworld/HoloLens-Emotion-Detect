using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CognitiveServices;

public class HUD : MonoBehaviour
{

    public Text UserBio;
    public Text UserState;
    public Text Status;
    public string faceAPIKey = "--face-api-key-here--";
    public string faceAPIEndPoint = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0";
    public string faceAPIParams = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

    IEnumerator coroutine;

    WebCamTexture webcam;

    // Use this for initialization
    void Start()
    {
        Status.text = "Initializing";
        webcam = new WebCamTexture();
        webcam.requestedFPS = 5;
        webcam.requestedHeight = 504;
        webcam.requestedWidth = 896;
        webcam.Play();
        Debug.LogFormat("webcam: {0} {1} x {2}", webcam.deviceName, webcam.width, webcam.height);

        if (null == UserBio || null == UserState)
        {
            //NOOP
        }
        else
        {
            UserBio.text = "User Bio:\n...";
            UserState.text = "User State:...";
        }
        Status.text = "Initialized";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Texture2D TakePhoto()
    {
        Status.text = "Taking Photo";
        Debug.Log("Take Photo");
        Texture2D webcamImage = new Texture2D(webcam.width, webcam.height);
        webcamImage.SetPixels(webcam.GetPixels());
        webcamImage.Apply();
        return webcamImage;
    }

    public void TakePhotoToPreview(RawImage target)
    {
        Texture2D image = TakePhoto();
        byte[] bytes = image.EncodeToJPG();
        target.texture = image;
        coroutine = ProcessImage(bytes);
        StartCoroutine(coroutine);
    }

    private string getFaceDetectURL()
    {
        string url = faceAPIEndPoint;
        if (!url.EndsWith("/")) url += "/";
        url += "detect?";
        url += faceAPIParams;
        return url;
    }

    IEnumerator ProcessImage(byte[] image)
    {
        Status.text = "Analyzing";
        UserState.text = "User State:...";
        var headers = new Dictionary<string, string>()
        {
            { "Ocp-Apim-Subscription-Key", faceAPIKey},
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(getFaceDetectURL(), image, headers);
        yield return www;

        if (www.error != null && www.error != "")
        {
            Status.text = "Error";
            Debug.Log(www.error);
            yield break;
        }

        try
        {
            var wrapper = JsonUtility.FromJson<Wrapper>("{\"faces\":" + www.text + "}");

            List<string> faceIds = new List<string>();
            foreach (var face in wrapper.faces)
            {
                faceIds.Add(string.Format("{0} with emotion {1}", face.faceId, GetEmotion(face)));
            }
            Status.text = string.Format("Found {0}", faceIds.Count);
            if (0 < faceIds.Count)
            {
                UserState.text = GetEmotion(wrapper.faces[0]);
            }
        }
        catch (Exception ex)
        {
            Status.text = "Error";
            Debug.Log(ex.Message);
        }
    }

    public string GetEmotion(Face face)
    {
        string ret = "Unknown";
        if (null != face && null != face.faceAttributes && null != face.faceAttributes.emotion)
        {
            ret = face.faceAttributes.emotion.GetLikely();
        }
        return ret;
    }

}
