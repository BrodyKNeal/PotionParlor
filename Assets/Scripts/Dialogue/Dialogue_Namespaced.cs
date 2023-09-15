//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

namespace Dialogue_namespace
{
    //Defining some custom classes.
    [System.Serializable]
    //All the info contained per dialogue line. Doesn't have to just be text, can be images, sounds, etc.
    public class Line
    {
        public string text; //The line of dialogue.
    }

    [System.Serializable]
    public class Dialogue //the container for multiple dialogue lines
    {
        public Line[] lines;
    }
}
