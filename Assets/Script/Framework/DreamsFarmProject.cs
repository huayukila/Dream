namespace Framework.Farm
{
    public class DreamsFarmProject : Architecture<DreamsFarmProject>
    {
        protected override void Init()
        {
            RegisterModel<ISaveDataModel>(new SaveDataModel());
            RegisterModel<IItemConfigModel>(new ItemConfigModel());
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterSystem<ISaveSystem>(new SaveSystem());
            RegisterSystem<IInventorySystem>(new InventorySystem());
        }
    }
}