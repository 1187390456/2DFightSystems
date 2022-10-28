namespace InventorySystem
{
    public class InventorySlotStateChangedArgs
    {
        // 构造库存插槽存储状态
        public InventorySlotStateChangedArgs(ItemStack newState, bool active)
        {
            NewState = newState;
            Active = active;
        }

        public ItemStack NewState { get; }
        public bool Active { get; }
    }
}