﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class TeamController //Team
    {
        public List<TeamEntity> List()
        {
            using (var context = new Context())
            {
                return context.TeamEntitys.ToList();
            }
        }
        public TeamEntity FindByID(int id)
        {
            using (var context = new Context())
            {
                return context.TeamEntitys.Find(id);
            }
        }
    }
}
