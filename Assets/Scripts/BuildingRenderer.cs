using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRenderer : MonoBehaviour
{
    public Transform[] verticalPrefab;
    public Transform[] horizontalPrefab;
    Transform buildingFolder;

    public void Render(Building building)
    {
        buildingFolder = new GameObject("Building").transform;
        foreach (Story story in building.Stories)
        {
            RenderStory(story, buildingFolder);
        }
    }

    private void RenderStory(Story story, Transform buildingFolder)
    {
        Transform storyFolder = new GameObject("Story " + story.Level).transform;
        storyFolder.SetParent(buildingFolder);
        RenderVertical(story, storyFolder);
        RenderHorizontal(story, storyFolder);
    }

    
    private void RenderVertical(Story story, Transform storyFolder)
    {
        
        for (int i = 0; i < story.RectList.Count; i++)
        {
            RectInt r = story.RectList[i];
            for (int x = r.min.x; x < r.max.x; x++)
            {
                for (int y = r.min.y; y < r.max.y; y++)
                {
                    //A wall
                    if (y == r.min.y)
                    {                        
                        Transform vertical = verticalPrefab[(int)story.RectVerticals[i][(x - r.min.x) * r.height]];
                        PlaceSouthWall(x, y, story.Level, storyFolder, vertical);
                    }
                    //B wall
                    if (x == r.min.x + r.size.x - 1)
                    {
                        Transform vertical = verticalPrefab[(int)story.RectVerticals[i][(x - r.min.x) * r.height + y - r.min.y]];
                        PlaceEastWall(x, y, story.Level, storyFolder, vertical);
                    }

                    //C wall
                    if (y == r.min.y + r.size.y - 1)
                    {
                        Transform vertical = verticalPrefab[(int)story.RectVerticals[i][(x - r.min.x) * r.height + r.height - 1]];
                        PlaceNorthWall(x, y, story.Level, storyFolder, vertical);
                    }

                    //D wall
                    if (x == r.min.x)
                    {
                        Transform vertical = verticalPrefab[(int)story.RectVerticals[i][y - r.min.y]];
                        PlaceWestWall(x, y, story.Level, storyFolder, vertical);
                    }
                }
            }
        }
    }

    private void PlaceSouthWall(int x, int y, int level, Transform storyFolder, Transform vertical)
    {
        Transform v = Instantiate(
            vertical,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -3f,
                    0.3f + level * 2.5f,
                    y * -3f - 0.5f
                    )
                ),
            Quaternion.Euler(0, 90, 0));
        v.SetParent(storyFolder);
    }

    private void PlaceEastWall(int x, int y, int level, Transform storyFolder, Transform vertical)
    {
        Transform v = Instantiate(
            vertical,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -3f - 2.5f,
                    0.3f + level * 2.5f,
                    y * -3f
                    )
                ),
            Quaternion.identity);
        v.SetParent(storyFolder);
    }

    private void PlaceNorthWall(int x, int y, int level, Transform storyFolder, Transform vertical)
    {
        Transform v = Instantiate(
            vertical,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -3f,
                    0.3f + level * 2.5f,
                    y * -3f - 3f
                    )
                ),
            Quaternion.Euler(0, 90, 0));
        v.SetParent(storyFolder);
    }

    private void PlaceWestWall(int x, int y, int level, Transform storyFolder, Transform vertical)
    {
        Transform v = Instantiate(
            vertical,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -3f,
                    0.3f + level * 2.5f,
                    y * -3f
                    )
                ),
            Quaternion.identity);
        v.SetParent(storyFolder);
    }
        

    private void RenderHorizontal(Story story, Transform stoyFolder)
    {
        for (int i = 0; i < story.RectList.Count; i++)
        {
            for (int x = 0; x < story.RectList[i].width; x++)
            {
                for (int y = 0; y < story.RectList[i].height; y++)
                {
                    Transform horizontal = horizontalPrefab[(int)story.RectHorizontals[i][x * story.RectList[i].height + y]];
                    PlaceHorizontal(story.RectList[i].min.x + x, story.RectList[i].min.y + y, story.Level, horizontal, stoyFolder);
                }
            }
        }
    }


    private void PlaceHorizontal(int x, int y, int level, Transform horizontal, Transform storyFolder)
    {
        Transform h;
        h = Instantiate(
            horizontal,
            storyFolder.TransformPoint(
                new Vector3(
                        x * -3f + rotationOffset[1].x,
                        (level + 1) * 2.5f - 0.3f,
                        y * -3f + rotationOffset[1].z
                    )
                ),
            Quaternion.Euler(0f, rotationOffset[1].y, 0f)
            );
        h.SetParent(storyFolder);
    }

    Vector3[] rotationOffset = {
        new Vector3 (-3f, 270f, 0f),
        new Vector3 (0f, 0f, 0f),
        new Vector3 (0f, 90, -3f),
        new Vector3 (-3f, 180, -3f)
    };
}
