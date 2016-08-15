using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

    private Button b;
    private Button b2;

    public GameObject otherButton; //make it public so that you can see it in the inspector of the GameObject which the script is attached to

	// Use this for initialization
	void Start () {
        b = GetComponent<Button>(); //only works because button is what script is attached to
        b2 = otherButton.GetComponent<Button>();

        b.onClick.AddListener(message); //passing a function in a function
        b2.onClick.AddListener(delegate { paramFunction(5); }); //delegate is a type that when instantiated you can associate it with any method with return type
        //can invoke methods throught delegate instances
        //lambda expressions (using =>) can be assigned to delegates and be evaluated with the delegatename(param)
        //callback: when a pointer to executable code is passed as a parameter input to another code

       // GameObject positionx = GameObject.Find("PositionXInputField");
      //  var input = positionx.GetComponent<InputField>().text;


    }

    void message()
    {
        Debug.Log("Button Pressed");
        GameObject mycube = Resources.Load("wallprefab") as GameObject;
        Instantiate(mycube);
    }

    public void editorMEssage()
    {
        Debug.Log("assigned in the editor");
    }

    void paramFunction(int arg)
    {
        Debug.Log(arg);
    }


    void Update()
    {

    }
}
