using Framework;
using UnityEngine;

namespace Framework.Farm
{
    public interface IPlayerModel : IModel
    {
        InventoryUnit BackPack { get; }

        int FoodValue { get; set; }

        int WaterValue { get; set; }
        void SetBackPack(InventoryUnit unit);
    }

    public class PlayerModel : AbstractModel, IPlayerModel
    {
        public InventoryUnit BackPack { get; private set; }
        public int FoodValue { get; set; }
        public int WaterValue { get; set; }

        public void SetBackPack(InventoryUnit unit)
        {
            BackPack = unit;
        }

        protected override void OnInit()
        {
        }
    }
}