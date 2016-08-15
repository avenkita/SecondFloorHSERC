using UnityEngine;
using System.Collections;

public class Translation : MonoBehaviour {

    public float speed = 0.00000001f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed * Time.deltaTime, 0, 0);
	}
}
