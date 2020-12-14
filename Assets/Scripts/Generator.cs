using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Generator
{
    public static Building Generate(BuildingSettings settings)
    {
        return new Building(settings.BuildingSize.x, settings.BuildingSize.y, GenStories(settings));
    }

    static List<Story> GenStories(BuildingSettings settings)
    {        
        int numberOfStories = Random.Range(settings.MinNumberOfStories, settings.MaxNumberOfStories);
        List<Story> stories = new List<Story>();
        List<RectInt> bounds = new List<RectInt>();

        bounds.Add(new RectInt(0, 0, settings.BuildingSize.x, settings.BuildingSize.y));
        stories.Add(GenStory(settings, bounds, 1));

        for (int i = 1; i < numberOfStories; i++)
        {
            Story prevStory = stories[stories.Count - 1];
            
            stories.Add(GenStory(settings, prevStory.RectList, i + 1));
        }

        for (int i = 0; i < stories.Count()-1; i++)
        {
            FixRoof(stories[i + 1], stories[i]);
        }

        return stories;
    }

    static Story GenStory(BuildingSettings settings, List<RectInt> bounds, int level)
    {       
        int numberOfRects = Random.Range(settings.MinNumberOfRooms, settings.MaxNumberOfRooms);
        int rectWidth = Random.Range(settings.MinRoomWidth, settings.MaxRoomWidth);
        int rectHeight = Random.Range(settings.MinRoomHeight, settings.MaxRoomHeight);

        int start = Random.Range(0, bounds.Count);

        int rectX = Random.Range(bounds[start].x, bounds[start].x + bounds[start].width);
        int rectY = Random.Range(bounds[start].y, bounds[start].y + bounds[start].height);

        List<RectInt> rectList = new List<RectInt>();

        //first rect
        for (int i = 0; i < 50000; i++)
        {            
            rectWidth = Random.Range(settings.MinRoomWidth, settings.MaxRoomWidth);
            rectHeight = Random.Range(settings.MinRoomHeight, settings.MaxRoomHeight);
            start = Random.Range(0, bounds.Count);
            rectX = Random.Range(bounds[start].x, bounds[start].x + bounds[start].width);
            rectY = Random.Range(bounds[start].y, bounds[start].y + bounds[start].height);

            RectInt rect = new RectInt(rectX, rectY, rectWidth, rectHeight);

            List<Vector2Int> rectCoords = new List<Vector2Int>();

            for (int x = rect.x; x < rect.x + rect.width; x++)
            {
                for (int y = rect.y; y < rect.y + rect.height; y++)
                {
                    rectCoords.Add(new Vector2Int(x, y));
                }

            }
            
            bool[] contains;
            contains = new bool[rectCoords.Count];
            
            for (int c = 0; c < rectCoords.Count; c++)
            {                
                foreach (RectInt r in bounds)
                {                    
                    if (rectCoords[c].x >= r.x && rectCoords[c].x < r.x + r.width && rectCoords[c].y >= r.y && rectCoords[c].y < r.y + r.height)
                    {
                        contains[c] = true;
                        break;
                    }
                    else
                    { contains[c] = false; }
                }
            }

            if (contains.All(x => x))
            {
                rectList.Add(rect);                
                break;
            }
        }              

        //rest of the rects
        if (rectList.Count > 0)
        {
            for (int rects = 0; rects < numberOfRects; rects++)
            {

                for (int i = 0; i < 50000; i++)
                {

                    rectWidth = Random.Range(settings.MinRoomWidth, settings.MaxRoomWidth);
                    rectHeight = Random.Range(settings.MinRoomHeight, settings.MaxRoomHeight);
                    int p = Random.Range(1, 3);

                    RectInt prev = rectList[rectList.Count - 1];
                    RectInt newRect;

                    newRect = (p == 1) ? (new RectInt(prev.x + prev.width, prev.y, rectWidth, rectHeight)) : (new RectInt(prev.x, prev.y + prev.height, rectWidth, rectHeight));

                    List<Vector2Int> rectCoords = new List<Vector2Int>();

                    for (int x = newRect.x; x < newRect.x + newRect.width; x++)
                    {
                        for (int y = newRect.y; y < newRect.y + newRect.height; y++)
                        {
                            rectCoords.Add(new Vector2Int(x, y));
                        }

                    }

                    bool[] contains;
                    contains = new bool[rectCoords.Count];

                    for (int c = 0; c < rectCoords.Count; c++)
                    {
                        foreach (RectInt r in bounds)
                        {
                            if (rectCoords[c].x >= r.x && rectCoords[c].x < r.x + r.width && rectCoords[c].y >= r.y && rectCoords[c].y < r.y + r.height)
                            {
                                contains[c] = true;
                                break;
                            }
                            else
                            { contains[c] = false; }
                        }
                    }

                    if (contains.All(x => x))
                    {
                        rectList.Add(newRect);
                        break;
                    }                    

                }
            }        
    }
                
        List<List<Horizontal>> rectHorizontals = GenRectHorizontals(rectList);
        List<List<Vertical>> rectVerticals = GenRectVerticals(settings, rectList, level);
        return new Story(rectList, rectHorizontals, rectVerticals, level);        
    }


    static void FixRoof(Story storyAbove, Story currentStory)
    {
        for (int i = 0; i < currentStory.RectList.Count(); i++)
        {
            for (int x = 0; x < currentStory.RectList[i].width; x++)
            {
                for (int y = 0; y < currentStory.RectList[i].height; y++)
                {            
                    foreach (RectInt rect in storyAbove.RectList)
                    {
                        Vector2Int coords = new Vector2Int(currentStory.RectList[i].min.x + x, currentStory.RectList[i].min.y + y);
                        if (rect.Contains(coords))
                        {
                            currentStory.RectHorizontals[i][x * currentStory.RectList[i].height + y] = Horizontal.Ceiling;
                        }
                    }
                }
            }
        }
    }

    static List<List<Horizontal>> GenRectHorizontals(List<RectInt> rectList)
    {
        List<List<Horizontal>> rectHorizontals = new List<List<Horizontal>>();

        foreach (RectInt r in rectList)
        {
            List<Horizontal> horizontals = new List<Horizontal>();
            for (int x = 0; x < r.width; x++)
            {
                for (int y = 0; y < r.height; y++)
                {
                    horizontals.Add(Horizontal.Roof);
                    
                }
            }
            
            rectHorizontals.Add(horizontals);

        }
        
        return rectHorizontals;
    }

    static List<List<Vertical>> GenRectVerticals(BuildingSettings settings, List<RectInt> rectList, int level)
    {        
        List<List<Vertical>> rectVerticals = new List<List<Vertical>>();

        foreach (RectInt r in rectList)
        {
            List<Vertical> verticals = new List<Vertical>();
            for (int x = 0; x < r.width; x++)
            {
                for (int y = 0; y < r.height; y++)
                {
                    verticals.Add(Vertical.Wall);
                    int rand = Random.Range(1, 100);
                    if (rand < settings.WindowSeed) { verticals[verticals.Count()  -1] = Vertical.Window ; }
                    rand = Random.Range(1, 100);
                    if (level == 1 && rand < settings.DoorSeed) { verticals[verticals.Count() - 1] = Vertical.Door; }
                    
                }
            }
            rectVerticals.Add(verticals);
        }
        return rectVerticals;
    }


    
}
