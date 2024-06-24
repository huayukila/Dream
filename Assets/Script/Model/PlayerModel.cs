using Framework;

public interface IPlayerModel : IModel
{
    StorageUnit BackPack { get; }
}

public class PlayerModel : AbstractModel, IPlayerModel
{
    public StorageUnit BackPack { get; private set; }

    protected override void OnInit()
    {
        //todo...
        BackPack = new StorageUnit(5);
        BackPack.Slots[0].Item = new Item(01, "boom", 20);
        BackPack.Slots[0].Nums = 30;
    }
}