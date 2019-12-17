using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door {

    private Room room1;
    private Room room2;
    private bool _isOpen;
    public bool isOpen { get { return _isOpen; } set { _isOpen = value; } }
    private LockingStrategy _lockStrategy;
    public LockingStrategy lockStrategy { get { return _lockStrategy; } set { _lockStrategy = value; } }
	
    public Door(Room room1, Room room2)
    {
        this.room1 = room1;
        this.room2 = room2;
        _isOpen = true;
        this.lockStrategy = null;
    }

    public Room getOtherRoom(Room fromRoom)
    {
        if (fromRoom == room1)
        {
            return room2;
        }
        else
        {
            return room1;
        }
    }

    public void close()
    {
        this._isOpen = (this.lockStrategy == null) ? false : !lockStrategy.mayClose();
    }

    public void open()
    {
        this._isOpen = (this.lockStrategy == null) ? true : lockStrategy.mayOpen();
    }

    public static Door connectRooms(Room room1, string exitName1, Room room2, string exitName2)
    {
        Door door = new Door(room1, room2);

        room1.setExit(exitName1, door);
        room2.setExit(exitName2, door);

        return door;
    }
}
