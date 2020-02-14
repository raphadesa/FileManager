namespace FileManager.Helpers.DragAndDrop
{
    public class DragAndDropHelper
    {
        public object Data {get; set;}        
        public string Zone {get; set;}
        public void StartDrag(object data, string zone)
        {            
            this.Data = data;
            this.Zone = zone;
        }

        public bool Accepts(string zone)
        {
            return this.Zone == zone;
        }
    }
}
