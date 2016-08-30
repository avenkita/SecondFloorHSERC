using UnityEngine;
using System.Collections;
using System.Collections.Generic; //this is where the List<T>() class comes from
using System.Xml;

//A code which can read an XML file with equipment information (XMLHSERC.xml) from the Resources folder and instantiate cubes representing the equiment pieces.


public class ReadObstacles : MonoBehaviour {
    
    //initializes a list of Wall type
    public static List<Equipment> ListofObjects = new List<Equipment>();

    // Use this for initialization
    void Start () {
        List<Equipment> EquipmentList = getObjects(); //EquipmentList is returned by the getObjects function
        GameObject mycube = Resources.Load("Wallprefab") as GameObject; //prefab is loaded

        for (int objnumber = 0; objnumber < EquipmentList.Count; objnumber++) //loops through all walls in wallslist
        {
            var currentobj = EquipmentList[objnumber];
            GameObject go = Instantiate(mycube) as GameObject; //all walls begin as simple cubes
            //Unity transform is updated with the values from the XML, simple cube has Wall dimensions
            go.transform.position = new Vector3(currentobj.PositionX, currentobj.PositionY, currentobj.PositionZ);
            go.transform.localScale = new Vector3(currentobj.ScaleX, currentobj.ScaleY, currentobj.ScaleZ);
            go.name = currentobj.EquipmentID;
            go.tag = currentobj.Movability;
            go.transform.parent = GameObject.Find(currentobj.RoomName).transform;
        }
        }

    public static void ReadObjectsXML()
    {
        //XML file is loaded and the XmlDocument class is used.
        TextAsset textXML = (TextAsset)Resources.Load("XMLHSERC", typeof(TextAsset));
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textXML.text);
        XmlNodeList transformList = xmldoc.GetElementsByTagName("HSERCenvironment"); //gets all innertext

        foreach (XmlNode transformInfo in transformList)
        {
            XmlNodeList transformcontent = transformInfo.ChildNodes; //gets each "Equipment" tag's innertext
            foreach (XmlNode transformItems in transformcontent)
            {
                XmlNodeList transformcontent2 = transformItems.ChildNodes; //gets each child tag in a "Equipment"
                Equipment equipmentobj = new Equipment(); //creating an instance of the Wall class which will store the necessary values.
                foreach (XmlNode transformItems2 in transformcontent2)
                {
                    //appending values from the XML into the fields of the class
                    if (transformItems2.Name == "EquipmentID") { equipmentobj.EquipmentID = transformItems2.InnerText; }
                    if (transformItems2.Name == "RoomName") { equipmentobj.RoomName = transformItems2.InnerText; }
                    if (transformItems2.Name == "Movability") { equipmentobj.Movability = transformItems2.InnerText; }
                    if (transformItems2.Name == "PositionX") { equipmentobj.PositionX = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "PositionY") { equipmentobj.PositionY = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "PositionZ") { equipmentobj.PositionZ = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "SizeX") { equipmentobj.ScaleX = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "SizeY") { equipmentobj.ScaleY = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "SizeZ") { equipmentobj.ScaleZ = float.Parse(transformItems2.InnerText); }
                    if (transformItems2.Name == "Notes") { equipmentobj.Notes = transformItems2.InnerText; }
                }
                ListofObjects.Add(equipmentobj); //the instance of the Wall class with all information from the point tags is appended into the list.
            }
        }
    }

    //the getWalls function initiates the ReadWallsXML function and returns the list of walls with info obtained from the XML file.
    public static List<Equipment> getObjects()
    {
        ReadObjectsXML();
        return ListofObjects;
    }
    
}
