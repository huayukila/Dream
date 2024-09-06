using Framework;
using UnityEngine;

namespace Framework.Farm
{
    public interface IPlayerModel : IModel
    {
        InventoryUnit BackPack { get; }
    }

    public class PlayerModel : AbstractModel, IPlayerModel
    {
        public InventoryUnit BackPack { get; private set; }

        protected override void OnInit()
        {
            //todo...
            BackPack = InventoryUnit.CreateStorageUnit(5);
        }
    }
}