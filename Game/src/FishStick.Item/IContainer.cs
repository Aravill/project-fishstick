namespace FishStick.Item
{
    interface IContainer : IItem
    {
        bool Locked { get; }
        List<IItem> Contents { get; set; }
        IItem? FindItem(string itemName);
        void RemoveItem(IItem item);
        void Unlock();
        void Lock();
    }
}
