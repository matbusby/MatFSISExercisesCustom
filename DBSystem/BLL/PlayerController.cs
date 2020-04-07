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
    public class PlayerController //Player
    {
        public PlayerEntity FindByPKID(int id)
        {
            using (var context = new Context())
            {
                return context.PlayerEntitys.Find(id);
            }
        }
        public List<PlayerEntity> List()
        {
            using (var context = new Context())
            {
                return context.PlayerEntitys.ToList();
            }
        }
        public List<PlayerEntity> FindByID(int id)
        {
            using (var context = new Context())
            {
                IEnumerable<PlayerEntity> results =
                    context.Database.SqlQuery<PlayerEntity>("Player_GetByTeam @ID"
                        , new SqlParameter("ID", id));
                return results.ToList();
            }
        }
        public int Add(PlayerEntity item)
        {
            using (var context = new Context())
            {
                context.PlayerEntitys.Add(item);
                context.SaveChanges();
                return item.PlayerID;

            }
        }
        public int Update(PlayerEntity item)
        {
            using (var context = new Context())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Delete(int productid)
        {
            using (var context = new Context())
            {
                var existing = context.PlayerEntitys.Find(productid);
                if (existing == null)
                {
                    throw new Exception("Record has been remove from database");
                }
                context.PlayerEntitys.Remove(existing);
                return context.SaveChanges();
            }
        }
    }
}
