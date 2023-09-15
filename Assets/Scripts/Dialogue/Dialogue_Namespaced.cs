//This allows all objects who want to display or create dialogue to do so.
//Just include "using Dialogue_namespace" in your script and you can create a dialogue object filled with whatever variables you want!
namespace Dialogue_namespace
{
    //Defining some custom classes.
    [System.Serializable]
    //All the info contained per dialogue line. Doesn't have to just be text, can include images, sounds, etc.
    public class Line
    {
        public string text; //The line of dialogue.
    }

    [System.Serializable]
    public class Dialogue //the container for multiple dialogue lines.
    {
        public Line[] lines;
    }
}
