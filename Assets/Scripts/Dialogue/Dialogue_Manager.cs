using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue_namespace;
using TMPro; //Allows us to edit textMeshPro

#if UNITY_EDITOR
using UnityEditor;
#endif
//This should manage the Dialogue Box game object. I assume you at least have a text mesh pro and an Image as a child of this object.

public class Dialogue_Manager : MonoBehaviour
{


    //Do some custom editing for text delay
    #region Editor_Fun_Stuff
    //Custom Unity Editor stuff
#if UNITY_EDITOR
    [CustomEditor(typeof(Dialogue_Manager))]
    public class Dialogue_Manager_Editor : Editor
    {
        bool allow_delay_customization = false;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Dialogue_Manager dialogue_manager = (Dialogue_Manager)target;

            //The actual custom part for the text delay
            EditorGUILayout.LabelField("Fun stuff");
            EditorGUILayout.BeginHorizontal();
            allow_delay_customization = EditorGUILayout.Toggle("Custom text delay?", allow_delay_customization);
            EditorGUILayout.EndHorizontal();
            if (allow_delay_customization) //only show text customization if box is checked
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Text Delay", GUILayout.MaxWidth(123));
                dialogue_manager.text_delay = EditorGUILayout.FloatField(dialogue_manager.text_delay);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                dialogue_manager.text_delay = 0.05f; //If you modify me, modify the other me in the construction fields!
            }
        }
    }
#endif
    #endregion

    [Header("Add child components")]
    public TextMeshProUGUI shown_line; //The actual gameObject that shows the dialogue text to the player. This should be a child.
    [Header("Fun stuff if you wanna mess around.")]
    private float text_delay = 0.05f; //Delay between characters appearing in WriteDialogue. If you modify me, modify the other me in the editor fun stuff!

    public int cur_line = 0; //The currently displayed line to the player. Best not to actually touch this
    public bool is_typing = false; //Is the dialogue still typing letter by letter? Used to prevent player from insta-skipping until dialogue line is done typing.

    //***end of fun stuff! DON'T TOUCH ANYTHING OUTSIDE OF ME


    //Local vriables within this script file.
    private Dialogue dialogue = new Dialogue();

    //Update the dialogue lines to show, then begin speaking from the start
    //This should be called by the parent Dialogue_Enabler object
    public void beginSpeaking(Dialogue received_lines)
    {
        dialogue = received_lines; //Update the 
        cur_line = 0;
        update_dialogue(); //Show the first line of dialogue
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //While the game is not typing out its dialogue and the dialogue has not reached the end, then go to next line
            //Otherwise, leave the cutscene
            if (!is_typing)
            {
                if (cur_line < dialogue.lines.Length - 1)
                {
                    cur_line++; //Go to the next line of dialogue
                    update_dialogue(); //The update everything
                }
                else
                {
                    LeaveCutscene();
                }
            }

        }
    }

    //Update the text and image displayed to the player based on cur_line
    //If you do anything custom, this is what you want to edit!
    void update_dialogue()
    {
        //*****DO NOT TOUCH
        shown_line.text = ""; //Reset the current dialogue.

        //Then start the Coroutine to write the text to the shown_line text object
        StartCoroutine(WriteDialogue(dialogue.lines[cur_line].text));
        // ***END OF DO NOT TOUCH**
    }

    //Writes the dialogue to the shown_line text object one character at a time.
    IEnumerator WriteDialogue(string text)
    {
        is_typing = true;
        int cur_dialogue_length = text.Length;
        int cur_letter = 0;
        for (int i = 0; i < cur_dialogue_length; i++)
        {
            shown_line.text += text[cur_letter]; //Add the next letter to the displayed text
            cur_letter++; //go to the next letter of the line

            yield return new WaitForSeconds(text_delay); //Wait for the assigned display time
        }

        //print("Done writing dialogue!");
        is_typing = false;

    }

    //When all dialogue has been exhausted 
    // leave the cutscene by disabling this object
    public void LeaveCutscene()
    {
        this.gameObject.SetActive(false); //Then disable the dialogue manager object
    }

}
