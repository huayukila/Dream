namespace Framework.Farm
{
    public class DreamsFarmProject : Architecture<DreamsFarmProject>
    {
        protected override void Init()
        {
            RegisterModel<IItemConfigModel>(new ItemConfigModel());
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterSystem<IInventorySystem>(new InventorySystem());
        }
    }
}