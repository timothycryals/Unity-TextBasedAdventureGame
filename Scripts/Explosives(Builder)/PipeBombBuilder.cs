using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBombBuilder : Explosive
{
    private ExplosiveProduct _product = new ExplosiveProduct();

    public override void buildCompound()
    {
        _product.Add(new Item("Gunpowder", 0));
    }

    public override void buildFuse()
    {
        _product.Add(new Item("Fuse", 0));
    }

    public override void buildShell()
    {
        _product.Add(new Item("Pipe Casing", 0));
    }

    public override Item GetResult(Player p)
    {
        if (_product.getPartsLength() == 3)
        {
            p.outputMessage("\nPipe bomb built and added to inventory");
            return new Item("Pipe Bomb", 3);
        }
        else
        {
            p.outputMessage("\nRequired parts have not been built");
            return null;
        }
    }
}
