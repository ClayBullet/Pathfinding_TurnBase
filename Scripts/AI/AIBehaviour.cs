using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

public abstract class AIBehaviour
{
   public AIBehaviour() { }

    public abstract EnemyOrders ordersEnemy { get;  }

    public abstract void FindAConcretPosition(TypeTiles typeTiles);
}

public class AISearchCover : AIBehaviour
{
    public override EnemyOrders ordersEnemy => throw new System.NotImplementedException();

    public override void FindAConcretPosition(TypeTiles typeTiles)
    {
       
    }
}

public class AITakeNPC : AIBehaviour
{
    public override EnemyOrders ordersEnemy => throw new System.NotImplementedException();

    public override void FindAConcretPosition(TypeTiles typeTiles)
    {
        throw new System.NotImplementedException();
    }
}


public static class AIOrdersProcessor
{
    private static Dictionary<EnemyOrders, AIBehaviour> _AIOrders = new Dictionary<EnemyOrders, AIBehaviour>();

    private static bool _initialized;

    private static void Initialize()
    {
        _AIOrders.Clear();

        var assembly = Assembly.GetAssembly(typeof(AIBehaviour));
       // var allAITypes = assembly.GetTypes().Where( t => )
    }
}
public enum EnemyOrders
{
    SearchCover,
    AimNPC
}