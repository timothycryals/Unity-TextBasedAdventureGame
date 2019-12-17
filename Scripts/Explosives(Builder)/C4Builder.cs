using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Builder : Explosive {

    private ExplosiveProduct _product = new ExplosiveProduct();

    public override void buildCompound()
    {
        _product.Add(new Item("c4 compound", 0));
    }

    public override void buildShell()
    {         
        _product.Add(new Item("c4 casing", 0));
    }

    public override void buildFuse()
    {
        _product.Add(new Item("c4 timer", 0));
    }

    public override Item GetResult(Player p)
    {
        if (_product.getPartsLength() == 3)
        {
            p.outputMessage("\nC4 built and added to inventory");
            return new Item("c4", 0);
        }
        else
        {
            p.outputMessage("\nRequired parts have not been built");
            return null;
        }
    }
}
