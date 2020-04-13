using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class GuardianController //Guardian
    {

        public GuardianEntity FindByPKID(int id)
        {
            using (var context = new Context())
            {
                return context.GuardianEntitys.Find(id);
            }
        }


        public List<GuardianEntity> List()
        {
            using (var context = new Context())
            {
                return context.GuardianEntitys.ToList();
            }
        }

        //public List<GuardianEntity> List()
        //{
        //    using (var context = new Context())
        //    {
        //        return context.GuardianEntitys.ToList();
        //    }
        //}


    }
}
