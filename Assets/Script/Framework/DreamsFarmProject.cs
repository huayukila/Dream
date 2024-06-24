namespace Framework
{
    public class DreamsFarmProject : Architecture<DreamsFarmProject>
    {
        protected override void Init()
        {
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterSystem<IStorageSystem>(new StorageSystem());
        }
    }
}