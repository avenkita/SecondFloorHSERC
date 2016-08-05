using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

    private Button b;
    private Button b2;

    public GameObject otherButton;

	// Use this for initialization
	void Start () {
        b = GetComponent<Button>(); //only works because button is what script is attached to
        b2 = otherButton.GetComponent<Button>();

        b.onClick.AddListener(message); //passing a function in a function
        b2.onClick.AddListener(delegate { paramFunction(5); });
	}

    void message()
    {
        Debug.Log("Button Pressed");
    }

    public void editorMEssage()
    {
        Debug.Log("assigned in the editor");
    }

    void paramFunction(int arg)
    {
        Debug.Log(arg);
    }
}
