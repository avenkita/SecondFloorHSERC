using UnityEngine;
using System.Collections;
using System.Collections.Generic; //this is where the List<T>() class comes from
using System.Xml;
using System.Text;
using System;
using System.ComponentModel;
using System.Linq;


//Code which can read Aishwarya's XML file (named MCLyellow.xml in Resources) and write into MCLspaceUnity.xml
//PROBLEM: line 27: the textwriter requires the correct directory path, so it must be modified to the user's computer info.
//Only has some of the walls, made primarily for demonstration during the meeting on July 25.

public class CompileMCLAish : MonoBehaviour {

    //initializes a list of SCWalls type
    public static List<SCWalls> ListofWalls = new List<SCWalls>();


    void Start()
    {
        //WallsList is returned by the getWalls function
        List<SCWalls> WallsList = getWalls();

        //instance of XmlTextWriter is created. File will load into Assets\Resources
        XmlTextWriter writer = new XmlTextWriter("C:\\Users\\Aishwarya\\Desktop\\SecondFloorHSERC\\SecondFloorMCL\\Assets\\Resources\\MCLspaceUnity.xml", Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartElement("Walls");

        for (int wallnumber = 0; wallnumber < WallsList.Count; wallnumber++) //loops through all walls in wallslist
        {
            var cw = WallsList[wallnumber];
            //for position, the mean of the two coordinate values is taken
            //for scale, the distance between the two coordinates is taken
            float xposition = (cw.Xcoord1 + cw.Xcoord2) / 2;
            float xscale = Mathf.Abs(cw.Xcoord1 - cw.Xcoord2);
            float zposition = (cw.Zcoord1 + cw.Zcoord2) / 2;
            float zscale = Mathf.Abs(cw.Zcoord1 - cw.Zcoord2);

            float angle; //declaring variable
            //this if statement evaluates walls which are diagonal (not constant in both x and y)
            if ((xscale > 0) && (zscale > 0))
            {
                float length = Mathf.Sqrt(Mathf.Pow(xscale, 2) + Mathf.Pow(zscale, 2));
                angle = Mathf.Atan((cw.Zcoord1 - cw.Zcoord2) / (cw.Xcoord1 - cw.Xcoord2)) * 180 / Mathf.PI;
                angle = -angle + 360; //add 360 to make the angle positive for string writing
                //wall is created with xscale as length and zscale as thickness, then rotated in y.
                xscale = length;
                zscale = 0.1f;
            }
            else //if the wall is constant in either x or y
            {
                if (xscale == 0) { xscale = zscale; zscale = 0.1f; angle = 90; }
                else { zscale = 0.1f; angle = 0; } //if (zscale == 0)
            }

            //adding in other properties (Unity compatible) into the current wall class
            cw.Name = (wallnumber + 1).ToString();
            cw.PositionX = xposition.ToString();
            cw.PositionY = "1.445";
            cw.PositionZ = zposition.ToString();
            cw.ScaleX = xscale.ToString();
            cw.ScaleY = "2.573039";
            cw.ScaleZ = zscale.ToString();
            cw.RotateX = "0";
            cw.RotateY = angle.ToString();
            cw.RotateZ = "0";

            //Writing into the XML
            writer.WriteStartElement("Wall");
            writer.WriteStartElement("Name"); writer.WriteString(cw.Name); writer.WriteEndElement();
            writer.WriteStartElement("RoomName"); writer.WriteString(cw.roomid); writer.WriteEndElement();
            writer.WriteStartElement("PositionX"); writer.WriteString(cw.PositionX); writer.WriteEndElement();
            writer.WriteStartElement("PositionY"); writer.WriteString(cw.PositionY); writer.WriteEndElement();
            writer.WriteStartElement("PositionZ"); writer.WriteString(cw.PositionZ); writer.WriteEndElement();
            writer.WriteStartElement("ScaleX"); writer.WriteString(cw.ScaleX); writer.WriteEndElement();
            writer.WriteStartElement("ScaleY"); writer.WriteString(cw.ScaleY); writer.WriteEndElement();
            writer.WriteStartElement("ScaleZ"); writer.WriteString(cw.ScaleZ); writer.WriteEndElement();
            writer.WriteStartElement("RotateX"); writer.WriteString(cw.RotateX); writer.WriteEndElement();
            writer.WriteStartElement("RotateY"); writer.WriteString(cw.RotateY); writer.WriteEndElement();
            writer.WriteStartElement("RotateZ"); writer.WriteString(cw.RotateZ); writer.WriteEndElement();
            writer.WriteEndElement();
        }
        writer.WriteEndElement(); //end "Walls"
        writer.Close(); //close Writer just in case there's a reading component.
    }


    public static void ReadWallsAXML()
    {
        //boolean variable varflag is used to separate the two points within each wall. 
        bool varflag = false;
        string roomid = " "; //initialize as an empty string
        //XML file is loaded and the XmlDocument class is used.
        TextAsset textXML = (TextAsset)Resources.Load("MCLyellow", typeof(TextAsset));
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textXML.text);

        XmlNodeList transformList = xml.GetElementsByTagName("simulatedworld");
        foreach (XmlNode transformInfo in transformList) //has just the simulated world tag!
        {
            XmlNodeList transformcontent = transformInfo.ChildNodes; //gets the tags inside the simulatedworld tag (rooms)
            foreach (XmlNode transformItems in transformcontent)
            {
                if (transformItems.Name != "rooms") { continue; }
                XmlNodeList transformcontent2 = transformItems.ChildNodes; //gets the tags inside the rooms tag (room)
                foreach (XmlNode transformItems2 in transformcontent2)
                {
                    XmlNodeList transformcontent3 = transformItems2.ChildNodes; //gets the tags inside the room tag (roomid and wall)
                    foreach (XmlNode transformItems3 in transformcontent3)
                    {
                        XmlNodeList transformcontent4 = transformItems3.ChildNodes; //gets the tags inside the wall tag (point and door)
                        if (transformItems3.Name == "roomid") { roomid = transformItems3.InnerText; continue; } //saves roomid innertext in string variable
                        SCWalls wallsimulation = new SCWalls(); //creates a new instance of SCWalls class
                        foreach (XmlNode transformItems4 in transformcontent4)
                        {
                            if ((varflag == false) && (transformItems4.Name == "point")) //when varflag is false, the first point in the XML is extracted in the loop
                            {
                                XmlNodeList transformcontent5 = transformItems4.ChildNodes; //gets the tags inside point tag (xcoord and ycoord)
                                foreach (XmlNode transformItems5 in transformcontent5)
                                {
                                    if (transformItems5.Name == "xcoord") { wallsimulation.Xcoord1 = float.Parse(transformItems5.InnerText); }
                                    if (transformItems5.Name == "ycoord") { wallsimulation.Zcoord1 = float.Parse(transformItems5.InnerText); }
                                }
                            }
                            if ((varflag == true) && (transformItems4.Name == "point")) //when varflag is true, the second point in the XML is extracted
                            {
                                XmlNodeList transformcontent5 = transformItems4.ChildNodes; //gets the tags inside point tag (xcoord and ycoord)
                                foreach (XmlNode transformItems5 in transformcontent5)
                                {
                                    if (transformItems5.Name == "xcoord") { wallsimulation.Xcoord2 = float.Parse(transformItems5.InnerText); }
                                    if (transformItems5.Name == "ycoord") { wallsimulation.Zcoord2 = float.Parse(transformItems5.InnerText); }
                                }
                            }
                            varflag = !varflag; //at the end of each loop, the varflag value is set to the opposite
                            if (transformItems4.Name == "door") { continue; }
                        }
                        wallsimulation.roomid = roomid; //roomid string is assigned to the field of the current class
                        ListofWalls.Add(wallsimulation); //the instance of the SCWalls class with all information from the point tags is appended into the list.
                    }
                }
            }
        }
    }

    //the getWalls function initiates the ReadWallsAXML function and returns the list of walls with info obtained from the XML file.
    public static List<SCWalls> getWalls()
    {
        ReadWallsAXML();
        return ListofWalls;
    }
}
