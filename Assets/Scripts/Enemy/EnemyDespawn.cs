using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnemyDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        //this.OnDespawnObject();
        EnemySpawner.Instance.Despawn(transform.parent);
    }
}
