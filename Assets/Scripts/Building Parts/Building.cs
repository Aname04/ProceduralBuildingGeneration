using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{

    Vector2Int size;
    List<Story> stories;

    public Vector2Int Size { get { return size; } }
    public List<Story> Stories { get { return stories; } }

    public Building(int sizeX, int sizeY, List<Story> stories)
    {
        size = new Vector2Int(sizeX, sizeY);
        this.stories = stories;
    }

    public override string ToString()
    {
        string building = "Building:(" + size.ToString() + "; " + stories.Count + ")\n";
        foreach(Story s in stories)
        {
            building += "\t" + s.ToString() + "\n";
        }
        return building;
    }

}
