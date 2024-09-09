using System;
using System.Collections.Generic;

namespace Framework.Farm
{
    public interface IInventoryModel : IModel
    {
        Dictionary<Guid, InventoryUnit> _unitsGroup { get; }
    }

    public class InventoryModel : AbstractModel, IInventoryModel
    {
        public Dictionary<Guid, InventoryUnit> _unitsGroup { get; private set; }

        protected override void OnInit()
        {
            _unitsGroup = new Dictionary<Guid, InventoryUnit>() { };
        }
    }
}