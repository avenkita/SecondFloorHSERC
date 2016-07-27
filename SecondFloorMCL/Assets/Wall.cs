using System.Xml;
using System.Xml.Serialization;

//A class that goes along with ReadWalls.cs


public class Wall
{
    public string Name { set; get; }
    public float PositionX { set; get; }
    public float PositionY { set; get; }
    public float PositionZ { set; get; }
    public float ScaleX { set; get; }
    public float ScaleY { set; get; }
    public float ScaleZ { set; get; }
    public float RotateX { set; get; }
    public float RotateY { set; get; }
    public float RotateZ { set; get; }
    public string roomid { set; get; }


 //   public float SwitchRotationY { set; get; }
 //   public float SwitchPositionX { set; get; }
 //   public float SwitchPositionY { set; get; }
 //   public float SwitchPositionZ { set; get; }
}