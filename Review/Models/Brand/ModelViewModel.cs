namespace Review.Models.Brand {
    public class ModelViewModel {

        public ModelViewModel() { } 
        public ModelViewModel( Model.Model model ) { 
            Id = model.Id;
            Name = model.Name;
            BrandId = model.BrandId;
        }

        public int Id { get; set; }
        public string Name { get; set; }    
        public int BrandId { get; set; }
    }
}
