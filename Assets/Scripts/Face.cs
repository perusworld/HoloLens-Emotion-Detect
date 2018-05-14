using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CognitiveServices {

    [Serializable]
    public class Wrapper
    {
        public Face[] faces;
    }


    [Serializable]
    public class Face
    {
        public string faceId;
        public FaceRectangle faceRectangle;
        public FaceAttributes faceAttributes;
    }

    [Serializable]
    public class FaceRectangle
    {
        public int height;
        public int left;
        public int top;
        public int width;
    }

    [Serializable]
    public class FaceAttributes
    {
        public Emotion emotion;
    }

    [Serializable]
    public class Emotion
    {
        public double anger;
        public double contempt;
        public double disgust;
        public double fear;
        public double happiness;
        public double neutral;
        public double sadness;
        public double surprise;

        public Dictionary<string, double> AsDictionary()
        {
            Dictionary<string, double> ret = new Dictionary<string, double>();
            ret.Add("anger", anger);
            ret.Add("contempt", contempt);
            ret.Add("disgust", disgust);
            ret.Add("fear", fear);
            ret.Add("happiness", happiness);
            ret.Add("neutral", neutral);
            ret.Add("sadness", sadness);
            ret.Add("surprise", surprise);
            return ret;
        }
        public string GetLikely()
        {
            var res = from entry in AsDictionary() orderby entry.Value descending select entry.Key;
            return res.First<string>();
        }
    }

}