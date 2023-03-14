using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MatveevWPFSessia1
{
    public partial class Product
    {
        public double costWithDiscount
        {
            get
            {
                return (double)(Convert.ToDouble(ProductCost) - (Convert.ToDouble(ProductCost) * ProductDiscountAmount / 100));
            }
        }


        public SolidColorBrush colorBackground
        {
            get
            {
                if (ProductDiscountAmount > 15)
                {
                    SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString("#7fff00");
                    return color;
                }
                return Brushes.White;
            }
        }

    }
}
