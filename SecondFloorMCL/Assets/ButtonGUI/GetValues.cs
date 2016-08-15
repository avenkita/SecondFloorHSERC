using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetValues : MonoBehaviour {

    public InputField xposinput;
    public InputField yposinput;
    public InputField zposinput;
    public InputField xscaleinput;
    public InputField yscaleinput;
    public InputField zscaleinput;
    

    // Use this for initialization
    void Start () {
        Button cancel = GameObject.Find("Cancel").GetComponent<Button>();
        Button okay = GameObject.Find("Okay").GetComponent<Button>();
        xposinput = GameObject.Find("PositionXInputField").GetComponent<InputField>();
        yposinput = GameObject.Find("PositionYInputField").GetComponent<InputField>();
        zposinput = GameObject.Find("PositionZInputField").GetComponent<InputField>();
        xscaleinput = GameObject.Find("ScaleXInputField").GetComponent<InputField>();
        yscaleinput = GameObject.Find("ScaleYInputField").GetComponent<InputField>();
        zscaleinput = GameObject.Find("ScaleZInputField").GetComponent<InputField>();

        cancel.onClick.AddListener(Clear); //add a non persistent listener to the event
        okay.onClick.AddListener(PlaceCube);
    }
	
	// Update is called once per frame
	void Update () {
     //   if (Input.GetButtonDown("Cancel"))
    //    {
    //        Debug.Log("Cancel was clicked");
     //   }
    }

    void display()
    {
        Debug.Log("Cancel Button Pressed");
    }

    void Clear()
    {
        xposinput.text = ""; //getting it from the inputfield component. If the text component gameobject is accesse, 
        yposinput.text = "";
        zposinput.text = "";
        xscaleinput.text = "";
        yscaleinput.text = "";
        zscaleinput.text = "";
    }

    void PlaceCube()
    {
        try //to prevent errors from not being able to parse the string into a float (say if someone put in a letter or other illegal character)
        {
            Vector3 posvec = new Vector3(float.Parse(xposinput.text), float.Parse(yposinput.text), float.Parse(zposinput.text));
            Vector3 scalevec = new Vector3(float.Parse(xscaleinput.text), float.Parse(yscaleinput.text), float.Parse(zscaleinput.text));
            GameObject cube = Resources.Load("Wallprefab") as GameObject;
            GameObject c = Instantiate(cube);
            c.transform.position = posvec;
            c.transform.localScale = scalevec;
            c.name = "new object";
            Clear();
        }
        catch
        {
            Clear();
        }
    }
}
