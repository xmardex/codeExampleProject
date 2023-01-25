public interface IItem
{
    public ItemType ItemType {get;}
    public abstract void Equip();
    public abstract void Unequip();
}