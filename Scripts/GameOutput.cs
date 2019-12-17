using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOutput {
    private Text _output;
    
    public GameOutput(Text output)
    {
        _output = output;
    }

    public void sendLine(string line)
    {
        _output.text += line;
    }

    public void clear()
    {
        _output.text = "";
    }
}
