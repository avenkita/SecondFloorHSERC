using UnityEngine;
using System.Collections;
using System.Collections.Generic; //this is where the List<T>() class comes from
using System.Xml;

public class ReadMCL : MonoBehaviour {

    public static List<Wall> ListofWalls = new List<Wall>();


    void Start()
    {
        List<Wall> WallsList = getWalls();

        GameObject mycube = Resources.Load("Wallprefab") as GameObject;
        string roomfind = " ";
        for (int wallnumber = 0; wallnumber < WallsList.Count; wallnumber++)
        {
            var cwall = WallsList[wallnumber];
            GameObject go = Instantiate(mycube) as GameObject;
            go.transform.position = new Vector3(cwall.PositionX, cwall.PositionY, cwall.PositionZ);
            go.transform.localScale = new Vector3(cwall.ScaleX, cwall.ScaleY, cwall.ScaleZ);
            go.transform.Rotate(cwall.RotateX, cwall.RotateY, cwall.RotateZ);
            go.name = cwall.Name;
            //go.tag = cwall.roomid;
            string oldroomfind = roomfind;
            roomfind = cwall.roomid;
            
            if (oldroomfind != roomfind)
            {
                GameObject roomparent = new GameObject(roomfind);
                go.transform.parent = roomparent.transform;
            }
            else
            {
                go.transform.parent = GameObject.Find(oldroomfind).transform;
            }
        }

        /*
        #region parenting

        GameObject[] cp = GameObject.FindGameObjectsWithTag("Client Preparation Room");
        GameObject clientpreparation = new GameObject("Client Preparation");
        foreach (GameObject wall in cp) { wall.transform.parent = clientpreparation.transform; }

        GameObject[] sc = GameObject.FindGameObjectsWithTag("SmartCondo");
        GameObject smartcondo = new GameObject("SmartCondo");
        foreach (GameObject wall in sc) { wall.transform.parent = smartcondo.transform; }

        GameObject[] m1 = GameObject.FindGameObjectsWithTag("Mobility1");
        GameObject mobility1 = new GameObject("Mobility 1");
        foreach (GameObject wall in m1) { wall.transform.parent = mobility1.transform; }

        GameObject[] m2 = GameObject.FindGameObjectsWithTag("Mobility2");
        GameObject mobility2 = new GameObject("Mobility 2");
        foreach (GameObject wall in m2) { wall.transform.parent = mobility2.transform; }

        GameObject[] m3 = GameObject.FindGameObjectsWithTag("Mobility3");
        GameObject mobility3 = new GameObject("Mobility 3");
        foreach (GameObject wall in m3) { wall.transform.parent = mobility3.transform; }

        GameObject[] sb = GameObject.FindGameObjectsWithTag("Sound Booth");
        GameObject soundbooth = new GameObject("Sound Booth");
        foreach (GameObject wall in sb) { wall.transform.parent = soundbooth.transform; }

        #endregion
        //could also use if statement within the forloop maybe?
        //for the FindGameObjectWithTag function, must all gameobjects be tagged? Can't leave them untagged?

    */

    }


    public static void ReadWallsXML()
    {
       // TextAsset textXML = (TextAsset)Resources.Load("MCLspaceUnity", typeof(TextAsset));
        TextAsset textXML = (TextAsset)Resources.Load("secondfloor", typeof(TextAsset));
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textXML.text);
        XmlNodeList transformList = xmldoc.GetElementsByTagName("Walls");

        foreach (XmlNode transformInfo in transformList)
        {
            XmlNodeList transformcontent = transformInfo.ChildNodes;
            foreach (XmlNode transformItems in transformcontent)
            {
                XmlNodeList transformcontent2 = transformItems.ChildNodes;
                Wall w = new Wall();
                foreach (XmlNode transformItems2 in transformcontent2)
                {
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
                ListofWalls.Add(w);
            }
        }
    }


    public static List<Wall> getWalls()
    {
        ReadWallsXML();
        return ListofWalls;
    }
}
