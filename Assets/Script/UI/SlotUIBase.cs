namespace Framework.Farm
{
    public abstract class SlotUIBase : ProjectCtrler
    {
        public ISlot Data { get; private set; }

        public void InitWithData(ISlot data)
        {
            Data = data;
            UpdateSlotUI();
        }

        public abstract void UpdateSlotUI();
    }
}