using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Building Gen/Building Settings")]
public class BuildingSettings : ScriptableObject
{
    public Vector2Int buildingSize;
    
    public int minNumberOfRooms;
    public int maxNumberOfRooms;
    public int minNumberOfStories;
    public int maxNumberOfStories;
    public int minRoomWidth;
    public int maxRoomWidth;
    public int minRoomHeight;
    public int maxRoomHeight;
    public int windowSeed;
    public int doorSeed;

    public Vector2Int BuildingSize { get => buildingSize; }
    public int MinNumberOfRooms { get => minNumberOfRooms; }
    public int MaxNumberOfRooms { get => maxNumberOfRooms; }
    public int MinNumberOfStories { get => minNumberOfStories; }
    public int MaxNumberOfStories { get => maxNumberOfStories; }
    public int MinRoomWidth { get => minRoomWidth; }
    public int MaxRoomWidth { get => maxRoomWidth; }
    public int MinRoomHeight { get => minRoomHeight; }
    public int MaxRoomHeight { get => maxRoomHeight; }
    public int WindowSeed { get => windowSeed; }
    public int DoorSeed { get => doorSeed; }
}
