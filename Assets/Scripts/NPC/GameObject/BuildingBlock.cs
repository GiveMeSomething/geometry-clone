using System;

public class BuildingBlock
{
    public static IBlockFlyweight GetFlyweight(Type type)
    {
        BlockFlyweightFactory flyweightFactory = BlockFlyweightFactory.GetInstance();
        var flyweight = flyweightFactory.GetFlyweight(type);

        if (flyweight == null)
        {
            throw new Exception($"Cannot resolve flyweight type for {type.ToString()}");
        }

        return flyweight;
    }
}

