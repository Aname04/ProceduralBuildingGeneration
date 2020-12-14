using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story
{

    RectInt bounds;
    int level;    
    List<RectInt> rectList;
    List<List<Horizontal>> rectHorizontals;
    List<List<Vertical>> rectVerticals;

    public RectInt Bounds { get => bounds; }
    public int Level { get => level; }
    public List<RectInt> RectList { get => rectList; }
    public List<List<Horizontal>> RectHorizontals { get => rectHorizontals; set => rectHorizontals = value; }
    public List<List<Vertical>> RectVerticals { get => rectVerticals; set => rectVerticals = value; }

    public Story(RectInt bounds)
    {
        this.bounds = bounds;
    }

    public Story(List<RectInt> rectList, List<List<Horizontal>> rectHorizontals, List<List<Vertical>> rectVerticals, int level)
    {
        this.rectList = rectList;
        this.rectHorizontals = rectHorizontals;
        this.rectVerticals = rectVerticals;
        this.level = level;
    }

    public override string ToString()
    {
        string story = "Story(" + bounds.ToString() + "):\n";
        //story += "\t\tHorizontals: ";
        //foreach(Horizontal h in horizontals)
        //{
        //    story += "\t" + h.ToString() + ",";
        //}
        //story += "\t\tVerticals: ";
        //foreach(Vertical v in verticals)
        //{
        //    story += "\t" + v.ToString() + ",";
        //}
        return story;
    }
}
