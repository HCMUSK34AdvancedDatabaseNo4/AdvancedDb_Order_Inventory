namespace AdvancedDb_Order_Inventory.Dtos
{
    public class ResponseMsg
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
