using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicalLayer.ApiConsumer
{
    public class Convertercate
    {
        public static Category CovertiCatego(RootCate Cate)
        {
            if (Cate.data == null)
            {
                return new Category();
            }

            Category c = new Category();
            c.Name = Cate.data.attributes.title;
            c.Description = Cate.data.attributes.description;
            c.ID = Convert.ToInt32(Cate.data.id);


            return c;
        }
    }

}

