using UnityEngine;
using System.Collections;
using System.Collections.Generic; //this is where the List<T>() class comes from
using System.Xml;

//A code which can read MCLspaceUnity.xml from Resources and instantiate walls. 
//Parenting of the objects is done through retaining the roomid and making it the name of the parent gameobject.


public class ReadMCLAish : MonoBehaviour {

    //initializes a list of Wall type
    public static List<Wall> ListofWalls = new List<Wall>();


    void Start()
    {
        List<Wall> WallsList = getWalls(); //WallsList is returned by the getWalls function
        GameObject mycube = Resources.Load("Wallprefab") as GameObject; //prefab is loaded
        string roomfind = " "; //intialize a string 
        for (int wallnumber = 0; wallnumber < WallsList.Count; wallnumber++) //loops through all walls in wallslist
        {
            var cwall = WallsList[wallnumber];
            GameObject go = Instantiate(mycube) as GameObject; //all walls begin as simple cubes
            //Unity transform is updated with the values from the XML, simple cube has Wall dimensions
            go.transform.position = new Vector3(cwall.PositionX, cwall.PositionY, cwall.PositionZ);
            go.transform.localScale = new Vector3(cwall.ScaleX, cwall.ScaleY, cwall.ScaleZ);
            go.transform.Rotate(cwall.RotateX, cwall.RotateY, cwall.RotateZ);
            go.name = cwall.Name;

            string oldroomfind = roomfind; //comparing roomid with old roomid
            roomfind = cwall.roomid;
            if (oldroomfind != roomfind) //if roomid for current wall is not the same as that of the previous wall
            {
                GameObject roomparent = new GameObject(roomfind); //make an empty GameObject with the same name as the roomid for current wall
                go.transform.parent = roomparent.transform; //parenting
            }
            else //if roomid for current wall is the same as that of the previous wall
            {
                go.transform.parent = GameObject.Find(oldroomfind).transform; //wall belongs to already created GameObject which parents all the walls within the current roomid
            }
        }
    }


    public static void ReadWallsXML()
    {
        //XML file is loaded and the XmlDocument class is used.
        TextAsset textXML = (TextAsset)Resources.Load("MCLspaceUnity", typeof(TextAsset));
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textXML.text);
        XmlNodeList transformList = xmldoc.GetElementsByTagName("Walls"); //gets all innertext

        foreach (XmlNode transformInfo in transformList)
        {
            XmlNodeList transformcontent = transformInfo.ChildNodes; //gets each "Wall" tag's innertext
            foreach (XmlNode transformItems in transformcontent)
            {
                XmlNodeList transformcontent2 = transformItems.ChildNodes; //gets each child tag in a "Wall"
                Wall w = new Wall(); //creating an instance of the Wall class which will store the necessary values.
                foreach (XmlNode transformItems2 in transformcontent2)
                {
                    //appending values from the XML into the fields of the class
                    if (transformItems2.Name == "Name") { w.Name = transformItems2.InnerText; }
                    if (transformItems2.Name == "RoomName") { w.roomid = transformItems2.InnerText; }
                    if (transformItems2.Name == "PositionX") { w.PositionX = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "PositionY") { w.PositionY = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "PositionZ") { w.PositionZ = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "ScaleX") { w.ScaleX = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "ScaleY") { w.ScaleY = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "ScaleZ") { w.ScaleZ = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "RotateX") { w.RotateX = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "RotateY") { w.RotateY = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "RotateZ") { w.RotateZ = float.Parse(transformItems2.InnerText); }
                }
                ListofWalls.Add(w); //the instance of the Wall class with all information from the point tags is appended into the list.
            }
        }
    }

    //the getWalls function initiates the ReadWallsXML function and returns the list of walls with info obtained from the XML file.
    public static List<Wall> getWalls()
    {
        ReadWallsXML();
        return ListofWalls;
    }
}
