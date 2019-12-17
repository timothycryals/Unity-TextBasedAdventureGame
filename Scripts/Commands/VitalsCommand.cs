using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalsCommand : Command {

    public VitalsCommand() : base()
    {
        this.name = "vitals";
    }

    override
    public bool execute(Player player)
    {
        player.showVitals();
        return false;
    }

}
