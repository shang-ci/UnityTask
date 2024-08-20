using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapGenerator : MonoBehaviour
{
    [Header("地图配置表")]
    public MapConfigSO mapConfig;

    [Header("预制体")]
    public Room roomPrefab;

    public LineRenderer linePrefab;

    private float screenHeight;

    private float screenWidth;

    private float columnWidth;

    private Vector3 generatePoint;

    public float border;

    private List<Room> rooms = new();

    private List<LineRenderer> lines = new();

    public List<RoomDataSO> roomDataList = new();

    private Dictionary<RoomType,RoomDataSO> roomDataDict = new();

    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;

        screenWidth = screenHeight * Camera.main.aspect;

        columnWidth = screenWidth / (mapConfig.roomBlueprints.Count + 1);

        foreach (var roomData in roomDataList)
        {
            roomDataDict.Add(roomData.roomType,roomData);
        }
    }

    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        //创建前一列房间列表
        List<Room> previousColumnRooms = new();

        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++)
        {
            var blueprint = mapConfig.roomBlueprints[column];

            var amount = UnityEngine.Random.Range(blueprint.min, blueprint.max);

            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);

            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);

            var newPosition = generatePoint;

            //创建当前房间列表
            List<Room> currentColumnRooms = new();

            var roomGapY = screenHeight / (amount + 1);

            //循环当前列的所有房间数量生成房间
            for (int i = 0; i < amount; i++)
            {
                //判断在最后一列时，boss的房间
                if (column == mapConfig.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;
                }
                else if (column != 0)
                {
                    newPosition.x = generatePoint.x + UnityEngine.Random.Range(-border / 2, border / 2);
                }

                newPosition.y = startHeight - roomGapY * i;


                //生成房间
                var room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);

                RoomType newtype = GetRandomRoomType(mapConfig.roomBlueprints[column].roomType);

                room.SetupRoom(column,i,GetRoomData(newtype));

                rooms.Add(room);

                currentColumnRooms.Add(room);
            }

            //判断当前列是否为第一列，如果不是则连接到上一列
            if (previousColumnRooms.Count > 0)
            {
                //创建两列表房间之间的连线
                CreateConnections(previousColumnRooms, currentColumnRooms);
            }

            previousColumnRooms = currentColumnRooms;
        }
    }

    private void CreateConnections(List<Room> column1, List<Room> column2)
    {
        HashSet<Room> connectedColumn2Room = new();

        foreach (var room in column1)
        {
            var targetRoom = ConnectToRandomRoom(room, column2);

            connectedColumn2Room.Add(targetRoom);
        }

        foreach (var room in column2)
        {
            if (!connectedColumn2Room.Contains(room))
            {
                ConnectToRandomRoom(room, column1);
            }
        }
    }

    private Room ConnectToRandomRoom(Room room, List<Room> column2)
    {
        Room targetRoom;

        targetRoom = column2[UnityEngine.Random.Range(0, column2.Count)];

        //创建房间之间的连线
        var line = Instantiate(linePrefab, transform);

        line.SetPosition(0, room.transform.position);

        line.SetPosition(1, targetRoom.transform.position);

        lines.Add(line);

        return targetRoom;
    }

    //重新生成地图
    [ContextMenu("ReGenerateRoom")]

    public void ReGenerateRoom()
    {
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }

        foreach (var item in lines)
        {
            Destroy(item.gameObject);
        }

        rooms.Clear();

        lines.Clear();

        CreateMap();
    }

    private RoomDataSO GetRoomData(RoomType roomType){
        return roomDataDict[roomType];
    }

    private RoomType GetRandomRoomType(RoomType flags){
        string[] options = flags.ToString().Split(',');

        string randomOption = options[UnityEngine.Random.Range(0,options.Length)];
        
        RoomType roomType = (RoomType)Enum.Parse(typeof(RoomType),randomOption);

        return roomType;
    }
}