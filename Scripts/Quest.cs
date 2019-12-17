using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CompletionState { COMPLETED, IN_PROGRESS, NA}
public enum QuestType { FIND, TALK, GO, FIGHT, BUILD, PLANT}

public class Quest {

    private string _name;
    public string name { get { return _name; }  set { _name = value; } }

    private QuestType _type;
    public QuestType type { get { return _type; } }

    private string _objective1;
    public string objective1 { get { return _objective1; } set { _objective1 = value; } }
    private CompletionState _obj1Status;
    public CompletionState obj1Status { get { return _obj1Status; } set { _obj1Status = value; } }
    private Item _obj1RequiredItem;
    public Item obj1RequiredItem { get { return _obj1RequiredItem; } }

    private string _objective2;
    public string objective2 { get { return _objective2; } set { _objective2 = value; } }
    private CompletionState _obj2Status;
    public CompletionState obj2Status { get { return _obj2Status; } set { _obj2Status = value; } }
    private Item _obj2RequiredItem;
    public Item obj2RequiredItem { get { return _obj2RequiredItem; } }

    private string _objective3;
    public string objective3 { get { return _objective3; } set { _objective3 = value; } }
    private CompletionState _obj3Status;
    public CompletionState obj3Status { get { return _obj3Status; } set { _obj3Status = value; } }
    private Item _obj3RequiredItem;
    public Item obj3RequiredItem { get { return _obj3RequiredItem; } }

    private bool _completed;
    public bool completed { get { return _completed; } set { _completed = value; } }

    private string endMessage;
    public string EndMessage { get { return endMessage; } }


    public Quest(string Name, QuestType Type, string obj1, string obj2, string obj3, Item obj1Item, Item obj2Item, Item obj3Item, string Message)
    {
        this.name = Name;
        this._type = Type;
        this.objective1 = obj1;
        this.objective2 = obj2;
        this.objective3 = obj3;
        this._obj1Status = CompletionState.IN_PROGRESS;
        this._obj2Status = CompletionState.IN_PROGRESS;
        this._obj3Status = CompletionState.IN_PROGRESS;
        this._obj1RequiredItem = obj1Item;
        this._obj2RequiredItem = obj2Item;
        this._obj3RequiredItem = obj3Item;
        this._completed = false;
        this.endMessage = Message;
    }

    public Quest(string Name, QuestType Type, string obj1, Item obj1Item, string Message)
    {
        this.name = Name;
        this._type = Type;
        this.objective1 = obj1;
        this.objective2 = null;
        this.objective3 = null;
        this._obj1Status = CompletionState.IN_PROGRESS;
        this._obj2Status = CompletionState.NA;
        this._obj3Status = CompletionState.NA;
        this._obj1RequiredItem = obj1Item;
        this._obj2RequiredItem = null;
        this._obj3RequiredItem = null;
        this._completed = false;
        this.endMessage = Message;
    }

    public Quest()
    {
        this.name = "Explore";
        this.objective1 = "Explore the city to find a way to escape.";
        this.objective2 = null;
        this.objective3 = null;
        this.obj1Status = CompletionState.NA;
        this.obj2Status = CompletionState.NA;
        this.obj3Status = CompletionState.NA;
        this._obj1RequiredItem = null;
        this._obj2RequiredItem = null;
        this._obj3RequiredItem = null;
        this._completed = false;
        this.endMessage = "";
    }

    public string completeObjective(string obj)
    {
        if (this.objective1 == obj)
        {
            this.obj1Status = CompletionState.COMPLETED;
            if (_obj1Status == CompletionState.COMPLETED && (_obj2Status == CompletionState.COMPLETED || _objective2 == null)
                && (_obj3Status == CompletionState.COMPLETED || _objective3 == null))
            {
                _completed = true;
                return "Quest Complete" + "\n" + this.endMessage;
            }
            else
            {
                return "Objective 1 has been completed";
            }
        }
        else if (this.objective2 == obj)
        {
            this.obj2Status = CompletionState.COMPLETED;
            if (_obj1Status == CompletionState.COMPLETED && _obj2Status == CompletionState.COMPLETED &&
                (_obj3Status == CompletionState.COMPLETED || _objective3 == null))
            {
                _completed = true;
                return "Quest Complete" + "\n" + this.endMessage;
            }
            else
            {
                return "Objective 2 has been completed";
            }
        }
        else if (this.objective3 == obj)
        {
            this.obj3Status = CompletionState.COMPLETED;
            if (_obj1Status == CompletionState.COMPLETED && _obj2Status == CompletionState.COMPLETED && _obj3Status == CompletionState.COMPLETED)
            {
                _completed = true;
                return "Quest Complete" + "\n" + this.endMessage ;
            }
            else
            {
                return "Objective 3 has been completed";
            }
        }
        else
        {
            return "No objectives complete";
        }
    }

    public string ShowQuest()
    {
        string temp = "";

        temp = "\n\n<b><i>" + this.name + "</i></b>\n" + this.objective1 + "-" + obj1Status.ToString();
        if (this.objective2 != null)
        {
            temp += "\n\n" + this.objective2 + "-" + obj2Status.ToString();
        }
        if (this.objective3 != null)
        {
            temp += "\n\n" + this.objective3 + "-" + obj3Status.ToString();
        }
        return temp;
    }
}
