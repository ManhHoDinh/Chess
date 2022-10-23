using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Chess
{
    internal class Program
    {  
        public static Form MyForm = new Form();
        public static Broad a = new Broad(MyForm,64);
        static void CreateForm()
        {
            MyForm.Size = new Size(800,1000);
            a.init();
        }

        static void Main(string[] args)
        {
            CreateForm();
            Application.Run(MyForm);
        }
    }
}
