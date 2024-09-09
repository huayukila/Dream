namespace Framework.Farm
{
    public class DreamsFarmProject : Architecture<DreamsFarmProject>
    {
        protected override void Init()
        {
            RegisterModel<ISaveDataModel>(new SaveDataModel());
            RegisterModel<IItemConfigModel>(new ItemConfigModel());
            RegisterModel<IInventoryModel>(new InventoryModel());
            RegisterModel<IPlayerModel>(new PlayerModel());

            RegisterSystem<ITimeSystem>(new TimeSystem());
            RegisterSystem<ISaveSystem>(new SaveSystem());
            RegisterSystem<IInventorySystem>(new InventorySystem());
        }
    }
}