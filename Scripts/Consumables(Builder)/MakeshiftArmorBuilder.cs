using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//concrete builder class for crafting Makeshift Armor
public class MakeshiftArmorBuilder : Makeshift {

    //parts are stored in the _product variable
    private MakeshiftProduct _product = new MakeshiftProduct();
    
    public override void buildPartA()
    {
        _product.Add(new Item("armor body", 0));
    }

    public override void buildPartB()
    {
        _product.Add(new Item("armor straps", 0));
    }

    public override void buildPartC()
    {
        _product.Add(new Item("armor carrier", 0));
    }

    //If all parts have been created, this will return Makeshift armor.
    public override Item GetResult(Player p)
    {
        if (_product.getPartsLength() == 3)
        {
            p.outputMessage("\nMakeshift armor built and equipped");
            return new Armor("makeshift armore",0, ArmorCondition.MODERATE);
        }
        else
        {
            p.outputMessage("\nRequired parts have not been built");
            return null;
        }
    }
}
