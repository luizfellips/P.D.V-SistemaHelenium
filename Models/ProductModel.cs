using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StockController.Models
{
    public class ProductModel
    {
        private int id;
        private string name;
        private string category;
        private int quantity;
        private double price;
        [DisplayName("ID do Produto")]
        [Required(ErrorMessage = "ID/Código do produto é requerido.")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [DisplayName("Nome do Produto")]
        [Required(ErrorMessage = "O Nome do produto é requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome do produto deve estar entre 3 e 50 caracteres.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DisplayName("Categoria do produto")]
        [Required(ErrorMessage = "A categoria do produto é requerida.")]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        [DisplayName("Quantidade do produto")]
        [Required(ErrorMessage ="A quantidade do produto é requerida.")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        [DisplayName("Preço do Produto")]
        [Required(ErrorMessage = "O Preço do produto é necessário")]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
